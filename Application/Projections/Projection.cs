using MediatR;
using Domain.Common;

namespace Application.Projections
{
    internal class Projection : IProjection
    {
        IMediator? Mediator { get; }

        public Projection(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public async Task Handle(string @eventStr)
        {
            var objActualEvent = EventParser.Parse(eventStr);
            await Mediator!.Publish(objActualEvent);
        }
    }
}