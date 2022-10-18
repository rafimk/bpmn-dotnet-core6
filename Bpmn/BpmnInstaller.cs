using Camunda.Worker;
using Camunda.Worker.Extensions;

namespace bpmn_dotnet_core6.Bpmn;

public static class BpmnInstaller
{
    public static IServiceCollection AddCamunda(this IServiceCollection services, string camundaRestApiUri)
    {
        services.AddSingleton(_ => new BpmnService(camundaRestApiUri));
        services.AddHostedService<BpmnProcessDeployService>();
            
        services.AddCamundaWorker(options =>
            {
                options.BaseUri = new Uri(camundaRestApiUri);
                options.WorkerCount = 1;
            })
            .AddHandler<NotifyCustomerTaskHandler>()
            .AddHandler<CreateInvoiceTaskHandler>();
            
        return services;
    }
}