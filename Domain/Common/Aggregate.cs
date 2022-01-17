using Newtonsoft.Json;
using Domain.Common;
using Microsoft.Extensions.Logging;

namespace Domain.Common
{
    public abstract class Aggregate : IRebuildFrom
    {
        private ILogger<Aggregate>? Logger { get; set;}
        
        protected ILoggerFactory? LoggerFactory {get; set;}

        public void AddLogger(ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
            this.Logger = LoggerFactory!.CreateLogger<Aggregate>();
        }

        private List<DomainEvent> DomainEvents {get;} = new List<DomainEvent>();

        protected void RaiseEvent<T>(Action<T> cbEventRaised) where T : DomainEvent, new()
        {   
            T t = new T();     
            cbEventRaised(t);
            ((dynamic)this).Apply(t);
            DomainEvents.Add(t);
        }

        public void RebuildFrom(string strDomainEvent)
        {
            var objActualEvent = EventParser.Parse(strDomainEvent);
                        
            ((dynamic)this).Apply(objActualEvent);
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