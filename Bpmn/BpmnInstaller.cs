using Camunda.Worker;
using Camunda.Worker.Extensions;
using bpmn_dotnet_core6;

namespace bpmn_dotnet_core6.Bpmn;

public static class BpmnInstaller
{
    private const string OptionsSectionName = "CamundaOptions";

    public static IServiceCollection AddCamunda(this IServiceCollection services)
    {
        services.Configure<CamundaOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var camundaOptions = configuration.GetOptions<CamundaOptions>(OptionsSectionName);
        services.AddSingleton(_ => new BpmnService(camundaOptions.camundaRestApiUri));
        services.AddHostedService<BpmnProcessDeployService>();
            
        services.AddCamundaWorker(options =>
            {
                options.BaseUri = new Uri(camundaOptions.camundaRestApiUri);
                options.WorkerCount = 1;
            })
            .AddHandler<NotifyCustomerTaskHandler>()
            .AddHandler<CreateInvoiceTaskHandler>();
            
        return services;
    }
}