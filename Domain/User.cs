using System.Security.Claims;

namespace bpmn_dotnet_core6.Domain;

public static class UserExtensions
{
    public static string CompanyCode(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(c => c.Type == "user-company")?.Value;
    }
      
}