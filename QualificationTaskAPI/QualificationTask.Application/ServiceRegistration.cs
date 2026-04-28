using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QualificationTask.Application.Behaviours;
using QualificationTask.Application.Features.Products.Validators;
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
                x.AddOpenBehavior(typeof(ValidationPipeline<,>));
            });

            services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();
        }
    }
}
