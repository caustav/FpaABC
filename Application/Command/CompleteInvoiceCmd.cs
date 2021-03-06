using Application.Common;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Command
{
    public class CompleteInvoiceCmd : IRequest<string>
    {
        public string InvoiceNumber { get; set; }  = default!;
    }

    public class CompleteInvoiceCmdHandler : IRequestHandler<CompleteInvoiceCmd, string>
    {
        ObjectBuilder builder;
        IEventStoreHandler eventStoreHandler;
        public CompleteInvoiceCmdHandler(ObjectBuilder builder, IEventStoreHandler eventStoreHandler)
        {
            this.builder = builder;
            this.eventStoreHandler = eventStoreHandler;
        }

        public async Task<string> Handle(CompleteInvoiceCmd request, CancellationToken cancellationToken)
        {
            var invoice = await builder.Build<Invoice>(request.InvoiceNumber, eventStoreHandler);
            if (invoice.InvoiceStatus != Invoice.Status.INVOICE_APPROVED)
            {
                throw new InvoiceIncompleteException();
            }

            invoice.Complete();
            await eventStoreHandler.Publish<DomainEvent>(invoice.EventsGenerated, request.InvoiceNumber);
            
            return invoice.Id;
        }
    }
}
