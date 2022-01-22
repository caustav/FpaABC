using MediatR;
using Mapster;
using Domain.DomainEvents;
using Application.Common;
using Domain;

namespace Application.Projections
{
    public class FpaProjection : INotificationHandler<InvoiceCreated>,
                                    INotificationHandler<InvoiceApproved>, 
                                        INotificationHandler<InvoiceCompleted>
    {
        IRepository<InvoiceDTO> repository;

        public FpaProjection(IRepository<InvoiceDTO> repository)
        {
            this.repository = repository;
        }

        public Task Handle(InvoiceCreated request, CancellationToken cancellationToken)
        {
            var dtoInvoice = request.Adapt<InvoiceDTO>();
            dtoInvoice.InvoiceStatus = Invoice.Status.INVOICE_PENDING;
            repository.Add(dtoInvoice);
            return Task.CompletedTask;
        }

        public async Task Handle(InvoiceApproved request, CancellationToken cancellationToken)
        {
            var dtoInvoice = await repository.Get(request.InvoiceNumber);
            if (dtoInvoice != null)
            {
                dtoInvoice.InvoiceStatus = Invoice.Status.INVOICE_APPROVED;
                await repository.Set(dtoInvoice);
            }
        }

        public async Task Handle(InvoiceCompleted request, CancellationToken cancellationToken)
        {
            var dtoInvoice = await repository.Get(request.InvoiceNumber);
            if (dtoInvoice != null)
            {
                dtoInvoice.InvoiceStatus = Invoice.Status.INVOICE_COMPLETED;
                await repository.Set(dtoInvoice);
            }
        }
    }
}