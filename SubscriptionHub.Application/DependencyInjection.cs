using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionHub.Application.Common.Behaviors;
using System.Reflection;

namespace SubscriptionHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
           
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddTransient(
                    typeof(IPipelineBehavior<,>),
                    typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}
