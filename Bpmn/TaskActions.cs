using Camunda.Api.Client.UserTask;

namespace bpmn_dotnet_core6.Bpmn;

public enum TaskActions
{
    NotifyNoHeroesAvailable,
    CreateOffer,
    AcceptOffer,
    RejectOffer
}
    
public static class UserTaskInfoExtension
{
    public static List<TaskActions> AvailableActions(this UserTaskInfo userTask)
    {
        return userTask.TaskDefinitionKey switch
        {
            "Task_PrepareOffer" => new List<TaskActions> { TaskActions.NotifyNoHeroesAvailable,TaskActions.CreateOffer },
            "Task_AcceptOffer" => new List<TaskActions>{ TaskActions.AcceptOffer, TaskActions.RejectOffer },
            _ => new List<TaskActions> { }
        };
    }
}