using Application.Common;
using Microsoft.Extensions.Logging;
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

            var settings = EventStoreClientSettings.Create("esdb://127.0.0.1:2111,127.0.0.1:2112,127.0.0.1:2113?tls=true&tlsVerifyCert=false");
            eventStoreClient = new EventStoreClient(settings);
        }

        public async Task Publish<TEvent>(IEnumerable<TEvent> @events, string streamId)
        {
            try
            {
                var eventsToBePublished = @events.Select(@event => 
                    {
                        Logger.LogInformation($"Event being published : {@event!.ToString()!}");
                        return new EventData(Uuid.NewUuid(), $"fpa-event", Encoding.UTF8.GetBytes(@event!.ToString()!));
                    });
                    
                await eventStoreClient.AppendToStreamAsync($"fpa-stream-{streamId}", StreamState.Any, eventsToBePublished);                 
            }
            catch(Exception e)
            {
                throw new Exception("Error in publishing events", e);
            } 
        }
        
        public async Task<IEnumerable<string>> GetEvents(string aggregateId)
        {
            var events = await ReadAllEventsFromAggregateStreamInstance(aggregateId);
            return events;
        }

        private async Task<IEnumerable<string>> ReadAllEventsFromAggregateStreamInstance(string streamId)
        {
            List<string> eventList = new List<string>();
            try
            {
                var streamResults = eventStoreClient.ReadStreamAsync(Direction.Forwards, $"fpa-stream-{streamId}", StreamPosition.Start);
                await foreach (var streamResult in streamResults)
                {
                        eventList.Add(Encoding.UTF8.GetString(streamResult.Event.Data.ToArray()));
                }                
            }
            catch (Exception e)
            {
                throw new Exception("Error in reading events from event store", e);
            }

            return eventList;
        }
    }
}