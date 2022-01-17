using Application.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Linq;
using EventStore.Client;
using System.Text;

namespace Infrastructure.EventStore
{
    public class EventStoreHandler : IEventStoreHandler
    {
        private ILogger<EventStoreHandler> Logger;

        private EventStoreClient eventStoreClient;
        
        public EventStoreHandler(ILogger<EventStoreHandler> logger)
        {
            this.Logger = logger;

            var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
            eventStoreClient = new EventStoreClient(settings);
        }

        public async Task Publish<TEvent>(IEnumerable<TEvent> @domainEvents)
        {
            var eventsToBePublished = @domainEvents.Select(@event => new EventData(Uuid.NewUuid(), "fpa-event",
                            Encoding.UTF8.GetBytes(@event!.ToString()!)));


            await eventStoreClient.AppendToStreamAsync("fpa-stream", StreamState.Any, eventsToBePublished);  
        }
        
        public Task<IEnumerable<string>> GetEvents(string aggregateId)
        {
            var e1 = new InvoiceCreated
            {
                Name = nameof(InvoiceCreated),
                InvoiceNumber = "I-980",
                InvoiceAmount = "2500 INR"
            };

            var e2 = new InvoiceApproved
            {
                Name = nameof(InvoiceApproved),
                InvoiceNumber = "I-980"
            };

            IEnumerable<string> eventList = new List<string>()
            {
                JsonConvert.SerializeObject(e1).ToString(),
                JsonConvert.SerializeObject(e2).ToString()
            };

            return Task.FromResult(eventList);
        }
    }
}