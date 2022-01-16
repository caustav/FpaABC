using Domain.Common;

namespace Application.Common
{
    public interface IEventStoreHandler
    {
        Task Publish<TEvent>(IEnumerable<TEvent> @domainEvents);
        IEnumerable<string> GetEvents(string aggregateId);
    }
}