namespace bpmn_dotnet_core6.Bpmn;

public class BpmnProcessDeployService : IHostedService
{
    private readonly BpmnService bpmnService;

    public BpmnProcessDeployService(BpmnService bpmnService)
    {
        this.bpmnService = bpmnService;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await bpmnService.DeployProcessDefinition();

        await bpmnService.CleanupProcessInstances();

        await bpmnService.DeployTestProcessDefinition();

        await bpmnService.CleanupTestProcessInstances();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}