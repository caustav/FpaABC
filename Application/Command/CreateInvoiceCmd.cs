using Application.Common;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Command
{
    public class CreateInvoiceCmd : IRequest<string>
    {
        public string InvoiceNumber { get; set; }  = default!;
        public string InvoiceAmount { get; set; } = default!;
    }

    public class CreateInvoiceCmdHandler : IRequestHandler<CreateInvoiceCmd, string>
    {
        ObjectBuilder builder;
        IEventStoreHandler eventStoreHandler;

        public CreateInvoiceCmdHandler(ObjectBuilder builder, IEventStoreHandler eventStoreHandler)
        {
            this.builder = builder;
            this.eventStoreHandler = eventStoreHandler;
        }

        public Task<string> Handle(CreateInvoiceCmd request, CancellationToken cancellationToken)
        {
            var invoice = builder.Build<CreateInvoiceCmd, Invoice>(request);
            invoice.Create(invoice.InvoiceNumber, invoice.InvoiceAmount);
            eventStoreHandler.Publish<DomainEvent>(invoice.EventsGenerated);
            return Task.FromResult(invoice.Id);
        }
    }
}
