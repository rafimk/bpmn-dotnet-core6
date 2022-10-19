using Camunda.Worker;
using Camunda.Worker.Extensions;
using bpmn_dotnet_core6;

namespace bpmn_dotnet_core6.Bpmn;

public static class BpmnInstaller
{
    private const string OptionsSectionName = "CamundaOptions";

    public static IServiceCollection AddCamunda(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CamundaOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var camundaOptions = configuration.GetOptions<CamundaOptions>(OptionsSectionName);
        services.AddSingleton(_ => new BpmnService(camundaOptions.CamundaRestApiUri));
        services.AddHostedService<BpmnProcessDeployService>();
            
        services.AddCamundaWorker(options =>
            {
                options.BaseUri = new Uri(camundaOptions.CamundaRestApiUri);
                options.WorkerCount = 1;
            })
            .AddHandler<NotifyCustomerTaskHandler>()
            .AddHandler<CreateInvoiceTaskHandler>();
            
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}