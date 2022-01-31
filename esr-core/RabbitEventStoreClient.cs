using DotNetCore.CAP;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace esr_core
{
    public class RabbitEventStoreClient : IESRClient
    {
        ICapPublisher capPublisher;

        EventDbContext eventDbContext;

        public RabbitEventStoreClient(ICapPublisher capPublisher, EventDbContext eventDbContext)
        {
            this.capPublisher = capPublisher;
            this.eventDbContext = eventDbContext;
        }

        public async Task Publish<T>(T t, string eventTag)
        {
            ArgumentNullException.ThrowIfNull(t);

            Random random = new Random();
            int eventId = random.Next();

            var strObject = t!.ToString();
            
            ArgumentNullException.ThrowIfNull(strObject);

            try
            {
                using (var transaction = eventDbContext.Database.BeginTransaction(capPublisher))
                {
                    var e = new Event
                    {
                        id = eventId,
                        value = strObject,
                        tag = eventTag,
                        dateTime = DateTime.Now.ToString()
                    };

                    await eventDbContext!.Event!.AddAsync(e);
                    await eventDbContext.SaveChangesAsync();

                    await capPublisher.PublishAsync("esr-core.event", e);        
                    await transaction.CommitAsync();
                }                
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<string> ReadEvents(string eventTag)
        {
            var events = (from e in eventDbContext.Event
                        where e.tag == eventTag select e).Select(e=> e.value);

            return events;
        }
    }   
}

