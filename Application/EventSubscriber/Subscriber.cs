using MediatR;
using Domain.Common;
using Microsoft.Extensions.Logging;

namespace Application.EventSubscriber
{
    internal class Subscriber : ISubscriber
    {
        IMediator? Mediator { get; }
        private readonly ILogger<Subscriber> logger;

        public Subscriber(IMediator mediator, ILogger<Subscriber> logger)
        {
            this.Mediator = mediator;
            this.logger = logger;
        }

        public async Task OnUpdate(string @eventStr)
        {
            var objActualEvent = EventParser.Parse(eventStr);
            string val = objActualEvent.Name;

            try
            {          
                await Mediator!.Publish(objActualEvent);
            }
            catch (System.Exception ex)
            {
                 logger.LogError(ex.Message);
            }

            logger.LogInformation(val);
        }
    }
}