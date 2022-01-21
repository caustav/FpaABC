using MediatR;
using Domain.Common;

namespace Application.EventSubscriber
{
    internal class Subscriber : ISubscriber
    {
        IMediator? Mediator { get; }

        public Subscriber(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public async Task OnUpdate(string @eventStr)
        {
            var objActualEvent = EventParser.Parse(eventStr);
            await Mediator!.Publish(objActualEvent);
        }
    }
}