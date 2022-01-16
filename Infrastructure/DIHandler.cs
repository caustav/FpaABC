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
            services.AddScoped<IRepository<InvoiceDTO>, MongoRepositoryInvoice>();
            services.AddScoped<IEventStoreHandler, MockedEventStoreHandler>();
            services.AddSingleton<IConfiguration, Configuration>();
            services.AddSingleton<MongoAdapter>();
        }
    }
}
