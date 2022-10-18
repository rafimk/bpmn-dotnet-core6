using System.Transactions;
using bpmn_dotnet_core6.Bpmn;
using bpmn_dotnet_core6.DAL;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace bpmn_dotnet_core6.Domain;

public class AcceptOffer
    {
        public class Command : IRequest<Unit>
        {
            public string TaskId { get; set; }
            public OrderId OrderId { get; set; }
        }
        
        public class Handler : IRequestHandler<Command, Unit>
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
                
                var order = await db.Orders
                    .Include(o=>o.Offer)
                    .ThenInclude(o => o.AssignedHero)
                    .Include(o => o.Customer)
                    .FirstAsync(o=>o.Id==request.OrderId,cancellationToken);

                order.AcceptOffer();
                
                await db.SaveChangesAsync(cancellationToken);

                await bpmnService.CompleteTask(request.TaskId, order);
                
                tx.Complete();
                
                return Unit.Value;
            }
        }
    }