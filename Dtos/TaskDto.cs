using bpmn_dotnet_core6.Bpmn;
using bpmn_dotnet_core6.Domain;
using Camunda.Api.Client.UserTask;

namespace bpmn_dotnet_core6.Dtos;

public class TaskDto
{
    public string TaskId { get; set; }
        
    public string TaskType { get; set; }
        
    public string TaskName { get; set; }
        
    public string Assignee { get; set; }
        
    public Guid? OrderId { get; set; }
        
    public string RequestedSuperpower { get; set; }
        
    public DateTime? OrderFrom { get; set; }
        
    public DateTime? OrderTo { get; set; }
        
    public string Customer { get; set; }
        
    public string OrderStatus { get; set; }
        
    public List<TaskActions> Actions { get; set; }

    public static TaskDto FromEntity(UserTaskInfo task, Order relatedOrder)
    {
        return new TaskDto
        {
            TaskId = task.Id,
            TaskType = task.TaskDefinitionKey,
            TaskName = task.Name,
            Assignee = task.Assignee,
            OrderId = relatedOrder?.Id.Value,
            RequestedSuperpower = relatedOrder?.Superpower.Name,
            OrderFrom = relatedOrder?.Period.From,
            OrderTo = relatedOrder?.Period.To,
            Customer = relatedOrder?.Customer.Name,
            OrderStatus = relatedOrder?.Status.ToString(),
            Actions = task.AvailableActions()
        };
    }
}