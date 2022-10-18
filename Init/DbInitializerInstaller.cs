namespace bpmn_dotnet_core6.Init;

public static class DbInitializerInstaller
{
    public static IServiceCollection AddDbInitializer(this IServiceCollection services)
    {
        services.AddScoped<DbSeed>();
        services.AddHostedService<DbInitializer>();
        return services;
    }
}