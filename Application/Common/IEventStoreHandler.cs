using Domain.Common;

namespace Application.Common
{
    public interface IEventStoreHandler
    {
        Task Publish<TEvent>(IEnumerable<TEvent> @events, string streamId);
        
        Task<IEnumerable<string>> GetEvents(string aggregateId);
    }
}