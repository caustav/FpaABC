using Newtonsoft.Json;
using Domain.Common;

namespace Domain.Common
{
    public abstract class Aggregate : ICanApply<string>
    {
        private List<DomainEvent> DomainEvents {get;} = new List<DomainEvent>();

        protected void Apply<T>(T t) where T : DomainEvent
        {
            DomainEvents.Add(t);
        }

        public void Apply(string strDomainEvent)
        {
            var objEvent = JsonConvert.DeserializeObject(strDomainEvent);
            ArgumentNullException.ThrowIfNull(objEvent);

            var objEventType = Type.GetType($"Domain.DomainEvents.{((dynamic)objEvent)?.Name}")!;
            ArgumentNullException.ThrowIfNull(objEventType);

            var objCustomEvent = (dynamic)Activator.CreateInstance(objEventType)!;
            ArgumentNullException.ThrowIfNull(objCustomEvent);

            var objActualEvent = objCustomEvent.ConvertFrom(strDomainEvent)!;
            ArgumentNullException.ThrowIfNull(objActualEvent);
                        
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