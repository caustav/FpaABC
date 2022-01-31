using DotNetCore.CAP;
using System;

namespace esr_core
{
    public class Subscriber : ICapSubscribe
    {        
        private readonly IESRObserver observer;
        public Subscriber(IESRObserver observer)
        {
            this.observer = observer;
        }

        [CapSubscribe("esr-core.event")]
        public void OnNotify(Event e)
        {
            observer.OnNotify(e.value);
            Console.WriteLine($@"{DateTime.Now} Subscriber invoked, Info: {e.value}");
        }

        [CapSubscribe("esr-core.event", Group = "group.test2")]
        public void OnNotify(Event e, [FromCap]CapHeader header)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber invoked");
        }
    }
}