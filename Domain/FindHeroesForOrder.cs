using bpmn_dotnet_core6.DAL;
using bpmn_dotnet_core6.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class FindHeroesForOrder
{
    public class Query : IRequest<ICollection<HeroDto>>
    {
        public OrderId OrderId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ICollection<HeroDto>>
    {
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<HeroDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == request.OrderId);
            var availableHeroes = await db.FindHeroForOrder(order);

            return availableHeroes.Select(HeroDto.FromEntity).ToList();
        }
    }
}
