using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Domain.Common
{
    public static class EventParser
    {
        public static dynamic Parse(string strDomainEvent)
        {
            var objEvent = JsonConvert.DeserializeObject(strDomainEvent);
            ArgumentNullException.ThrowIfNull(objEvent);

            var objEventType = Type.GetType($"Domain.DomainEvents.{((dynamic)objEvent)?.Name}")!;
            ArgumentNullException.ThrowIfNull(objEventType);

            var objCustomEvent = (dynamic)Activator.CreateInstance(objEventType)!;
            ArgumentNullException.ThrowIfNull(objCustomEvent);

            var objActualEvent = objCustomEvent.ConvertFrom(strDomainEvent)!;
            ArgumentNullException.ThrowIfNull(objActualEvent);

            return objActualEvent;
        }
    }
}