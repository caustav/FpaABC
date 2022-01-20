using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Projections;

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
            services.AddSingleton<IProjection, Projection>();
            // services.AddScoped<ApplicationMiddleware>();
        }

        public static void UseApplicationException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApplicationMiddleware>();
        }
    }
}
