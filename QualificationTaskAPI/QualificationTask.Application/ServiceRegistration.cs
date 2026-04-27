using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QualificationTask.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
