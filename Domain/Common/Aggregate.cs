using Newtonsoft.Json;
using Domain.Common;
using Microsoft.Extensions.Logging;

namespace Domain.Common
{
    public abstract class Aggregate : ICanApply<string>
    {
        private ILogger<Aggregate>? Logger { get; set;}
        
        protected ILoggerFactory? LoggerFactory {get; set;}

        public void AddLogger(ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
            this.Logger = LoggerFactory!.CreateLogger<Aggregate>();
        }

        private List<DomainEvent> DomainEvents {get;} = new List<DomainEvent>();

        protected void RaiseEvent<T>(Action<T> raiseCallback) where T : DomainEvent, new()
        {   
            T t = new T();     
            raiseCallback(t);
            Apply(t);
            DomainEvents.Add(t);
        }

        private void Apply(DomainEvent @event)
        {
            /// DO NOTHING
        }

        public void Apply(string strDomainEvent)
        {
            Logger?.LogInformation(strDomainEvent);

            var objActualEvent = EventParser.Parse(strDomainEvent);
                        
            Apply(objActualEvent);
        }

        public IEnumerable<DomainEvent> EventsGenerated
        {
            get
            {
                return new List<DomainEvent>(DomainEvents);
            }
        }
    }
}