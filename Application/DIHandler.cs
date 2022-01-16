using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DIHandler
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ConfigurationBehavior<,>));
            services.AddTransient<ObjectBuilder>();
            // services.AddScoped<ApplicationMiddleware>();
        }

        public static void UseApplicationException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApplicationMiddleware>();
        }
    }
}
