using Mapster;
using Domain.Common;
using Microsoft.Extensions.Logging;

namespace Application.Common
{
    public class ObjectBuilder
    {
        private ILogger<ObjectBuilder> Logger { get; init; } 

        private ILoggerFactory LoggerFactory { get; init; }   

        public ObjectBuilder(ILoggerFactory loggerFactory)
        {
            this.Logger = loggerFactory.CreateLogger<ObjectBuilder>();
            this.LoggerFactory = loggerFactory;
        }

        public TDst Build<TSrc, TDst>(TSrc srcObject) where TSrc : class
                                                      where TDst : class
        {
            return srcObject.Adapt<TDst>();
        }

        public T Build<T>(string id, IEventStoreHandler eventStoreHandler) where T : ICanApply<string>, new()
        {
            var eventsPublished = eventStoreHandler.GetEvents(id);
            var obj = new T();
            ((dynamic)obj).AddLogger(LoggerFactory);
            foreach (var @event in eventsPublished)
            {
                obj.Apply(@event);
            }
            return obj;
        }
    }
}
