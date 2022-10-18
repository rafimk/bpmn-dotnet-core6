using System.Transactions;
using bpmn_dotnet_core6.Bpmn;
using bpmn_dotnet_core6.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class CreateOffer
{
    public class Command : IRequest<Unit>
    {
        public string TaskId { get; set; }
        public OrderId OrderId { get; set; }
        public HeroId SelectedHero { get; set; }
    }

    public class Handler : IRequestHandler<Command,Unit>
    {
        private readonly HeroesDbContext db;
        private readonly BpmnService bpmnService;

        public Handler(HeroesDbContext db, BpmnService bpmnService)
        {
            this.db = db;
            this.bpmnService = bpmnService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);
            var candidateHero = await db.Heroes.FirstAsync(h => h.Id == request.SelectedHero, cancellationToken);

            var availableHeroes = await db.FindHeroForOrder(order);
            var heroAvailable = availableHeroes.Any(h => h.Id == candidateHero.Id);
            if (!heroAvailable)
            {
                throw new ApplicationException(($"Hero {candidateHero.Name} not available!"));
            }
            
            order.CreateOfferWithHero(candidateHero);

            await db.SaveChangesAsync(cancellationToken);
            
            await bpmnService.CompleteTask(request.TaskId, order);
            
            tx.Complete();
            
            return Unit.Value;
        }
    }
    
}
