using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.DAL;

public static class Dependency
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string cnnString)
        {
            // services.AddDbContext<HeroesDbContext>(options =>
            // {
            //     options.UseNpgsql(cnnString);
            // });

            services.AddDbContext<HeroesDbContext>(o => o.UseInMemoryDatabase("MyDatabase"));

            return services;
        }
    }