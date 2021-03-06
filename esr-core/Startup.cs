using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace esr_core
{
    public static class Startup
    {
        public static void AddRabbitEventStore(this IServiceCollection services, SystemConfiguration systemConfiguration)
        {
            services.AddDbContext<EventDbContext>(options =>
                options.UseSqlServer(systemConfiguration.SQLServerConnectionString));

            services.AddScoped<IESRClient, RabbitEventStoreClient>();
            services.AddScoped<Subscriber>();      

            services.AddCap(options =>
            {
                options.UseEntityFramework<EventDbContext>();
                // options.UseRabbitMQ(systemConfiguration.RabbitMqConnectionString);

                options.UseRabbitMQ(rabbit=>
                {
                    rabbit.HostName = systemConfiguration.RabbitMqConnectionString;
                    rabbit.UserName = "user";
                    rabbit.Password = "mWtTq9BJiC";
                });

                options.UseDashboard();
                options.FailedRetryCount = 5;
            });
        }
    }   
}

