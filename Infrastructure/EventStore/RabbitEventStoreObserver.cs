using esr_core;
using Application.EventSubscriber;

namespace Infrastructure.EventStore
{
    public class RabbitEventStoreObserver : IESRObserver
    {
        private readonly ISubscriber subscriber;
        public RabbitEventStoreObserver(ISubscriber subscriber)
        {
            this.subscriber = subscriber;
        }
        public async Task OnNotify(string strEvent)
        {
            await subscriber.OnUpdate(strEvent);
        }
    }
}