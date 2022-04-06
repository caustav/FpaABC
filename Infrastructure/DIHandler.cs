using Application.Common;
using Infrastructure.Configuraions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure.EventStore;
using esr_core;

namespace Infrastructure
{
    public static class DIHandler
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<InvoiceDTO>, MongoRepositoryInvoice>();
                        
            services.AddScoped<IESRObserver, RabbitEventStoreObserver>();
            services.AddRabbitEventStore(new SystemConfiguration
            {
                SQLServerConnectionString = "Server=mssql-svc.default.svc.cluster.local,1433;Initial Catalog=EventStore;User ID=SA;Password=abcd@123456;MultipleActiveResultSets=False;TrustServerCertificate=False;Connection Timeout=30;",
                RabbitMqConnectionString = "my-release-rabbitmq.default.svc.cluster.local"
            });
                        
            services.AddScoped<IEventStoreHandler, RabbitEventStoreHandler>();

            // services.AddScoped<IEventStoreHandler, EventStoreHandler>();
            // services.AddSingleton<IEventStoreSubscription, EventStoreSubscription>();
            services.AddSingleton<IConfiguration, Configuration>();
            services.AddSingleton<MongoAdapter>();

            // services.BuildServiceProvider().GetRequiredService<IEventStoreSubscription>().Enable().ContinueWith((task)=>
            // {
            //     if (task.Status == TaskStatus.Faulted)
            //     {
            //         throw new Exception(task.Exception!.InnerException!.Message ?? "Error in enabling subscription");
            //     }
            // });
        }
    }
}
