﻿using bpmn_dotnet_core6.Bpmn;
using bpmn_dotnet_core6.DAL;
using bpmn_dotnet_core6.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class GetSalesmanTasks
{
    public class Query : IRequest<ICollection<TaskDto>>
    {
        public string SalesmanLogin { get; set; }
    }
        
    public class Handler : IRequestHandler<Query, ICollection<TaskDto>>
    {
        private readonly BpmnService bpmnService;
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db, BpmnService bpmnService)
        {
            this.db = db;
            this.bpmnService = bpmnService;
        }

        public async Task<ICollection<TaskDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var tasks = await bpmnService.GetTasksForCandidateGroup("Sales", request.SalesmanLogin);
            var processIds = tasks.Select(t => t.ProcessInstanceId).ToList();

            var orders = await db.Orders
                .Include(o=>o.Customer)
                .Include(o=>o.Superpower)
                .Where(o => processIds.Contains(o.ProcessInstanceId))
                .ToListAsync(cancellationToken: cancellationToken);
            var processIdToOrderMap = orders.ToDictionary(o => o.ProcessInstanceId, o => o);

            return (from task in tasks
                    let relatedOrder = processIdToOrderMap.ContainsKey(task.ProcessInstanceId) ? processIdToOrderMap[task.ProcessInstanceId] : null
                    select TaskDto.FromEntity(task, relatedOrder))
                .ToList();
        }
    }
}