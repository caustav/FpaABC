using Application.Common;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Command
{
    public class ApproveInvoiceCmd : IRequest<string>
    {
        public string InvoiceNumber { get; set; } = default!;
    }

    public class ApproveInvoiceCmdHandler : IRequestHandler<ApproveInvoiceCmd, string>
    {
        ObjectBuilder builder;
        IEventStoreHandler eventStoreHandler;
        public ApproveInvoiceCmdHandler(ObjectBuilder builder, IEventStoreHandler eventStoreHandler)
        {
            this.builder = builder;
            this.eventStoreHandler = eventStoreHandler;
        }

        public Task<string> Handle(ApproveInvoiceCmd request, CancellationToken cancellationToken)
        {
            var invoice = builder.Build<Invoice>(request.InvoiceNumber, eventStoreHandler);
            invoice.Approve();
            eventStoreHandler.Publish<DomainEvent>(invoice.EventsGenerated);
            return Task.FromResult(invoice.Id);
        }
    }
}
