using bpmn_dotnet_core6.Domain;
using Camunda.Worker;
using MediatR;

namespace bpmn_dotnet_core6.Bpmn;

[HandlerTopics("Topic_NotifyCustomer_NoHeroes", LockDuration = 10_000)]
public class NotifyCustomerTaskHandler : ExternalTaskHandler
{
    private readonly IMediator bus;

    public NotifyCustomerTaskHandler(IMediator bus)
    {
        this.bus = bus;
    }

    public override async Task<IExecutionResult> Process(ExternalTask externalTask)
    {
        await bus.Send(new NotifyCustomer.Command
        {
            OrderId = new OrderId(Guid.Parse(externalTask.Variables["orderId"].AsString()))
        });
            
            
        return new CompleteResult {};
    }
}