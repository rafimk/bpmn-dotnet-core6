﻿using bpmn_dotnet_core6.DAL;
using bpmn_dotnet_core6.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class GetCustomerOrders
{
    public class Query : IRequest<List<OrderDto>>
    {
        public string CustomerCode { get; set; }
        public OrderStatus? Status { get; set; }
    }
        
    public class Handler : IRequestHandler<Query, List<OrderDto>>
    {
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db)
        {
            this.db = db;
        }

        public async Task<List<OrderDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var customerOrdersQuery = db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Superpower)
                .Include(o => o.Offer)
                .ThenInclude(oo => oo.AssignedHero)
                .Where(o => o.Customer.Code == request.CustomerCode);

            if (request.Status.HasValue)
            {
                customerOrdersQuery = customerOrdersQuery.Where(o => o.Status == request.Status.Value);
            }
                    
            var customerOrders = await customerOrdersQuery.ToListAsync(cancellationToken: cancellationToken);

            return customerOrders
                .Select(o=>OrderDto.FromEntity(o))
                .ToList();

        }
    }
}