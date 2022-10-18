using bpmn_dotnet_core6.DAL;
using bpmn_dotnet_core6.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class GetOrderDetails
{
    public class Query : IRequest<OrderDto>
    {
        public OrderId OrderId { get; set; }
    }

    public class Handler : IRequestHandler<Query, OrderDto>
    {
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db)
        {
            this.db = db;
        }

        public async Task<OrderDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);
                
            return OrderDto.FromEntity(order);
        }
    }
}