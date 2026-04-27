using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QualificationTask.Infrastructure.Persistance;

namespace QualificationTask.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QualificationTaskDbContext>(x =>
            {
                x.UseSqlite(configuration.GetConnectionString("Default"));
            });
        }
    }
}
