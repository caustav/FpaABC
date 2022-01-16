using Mapster;
using Domain.Common;

namespace Application.Common
{
    public class ObjectBuilder
    {
        public TDst Build<TSrc, TDst>(TSrc srcObject) where TSrc : class
                                                      where TDst : class
        {
            return srcObject.Adapt<TDst>();
        }

        public T Build<T>(string id, IEventStoreHandler eventStoreHandler) where T : ICanApply<string>, new()
        {
            var eventsPublished = eventStoreHandler.GetEvents(id);
            var obj = new T();
            foreach (var @event in eventsPublished)
            {
                obj.Apply(@event);
            }
            return obj;
        }
    }
}
