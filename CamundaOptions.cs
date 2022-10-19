namespace bpmn_dotnet_core6;

public class CamundaOptions
{
    public string Secret { get; set; } 
    public string[] AllowedOrigins { get; set; }
        
    public string CamundaRestApiUri { get; set; }
}