using esr_core;
using Application.Common;

namespace Infrastructure.EventStore
{
    public class RabbitEventStoreHandler : IEventStoreHandler
    {
        private readonly IESRClient esrClient;
        public RabbitEventStoreHandler(IESRClient esrClient)
        {
            this.esrClient = esrClient;
        }
        public async Task Publish<TEvent>(IEnumerable<TEvent> @events, string eventTag)
        {
            try
            {
                foreach (var e in @events)
                {
                    await esrClient.Publish<TEvent>(e, eventTag);
                }                
            }
            catch(Exception e)
            {
                throw new Exception("Error in publishing events", e);
            } 
        }
        
        public async Task<IEnumerable<string>> GetEvents(string eventTag)
        {
            var events = await Task.Run(()=>esrClient.ReadEvents(eventTag));
            return events;
        }        
    }
}