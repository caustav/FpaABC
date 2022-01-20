using Application.Common;
using Infrastructure.Configuraions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure.EventStore;

namespace Infrastructure
{
    public static class DIHandler
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<InvoiceDTO>, ConsoleRepository>();
            services.AddScoped<IEventStoreHandler, EventStoreHandler>();
            services.AddSingleton<IEventStoreSubscription, EventStoreSubscription>();
            services.AddSingleton<IConfiguration, Configuration>();
            services.AddSingleton<MongoAdapter>();

            services.BuildServiceProvider().GetRequiredService<IEventStoreSubscription>().Enable().ContinueWith((task)=>
            {
                if (task.Status == TaskStatus.Faulted)
                {
                    throw new Exception(task.Exception!.InnerException!.Message ?? "Error in enabling subscription");
                }
            });
        }
    }
}
