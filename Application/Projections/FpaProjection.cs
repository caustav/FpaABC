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

        public Task Handle(InvoiceApproved request, CancellationToken cancellationToken)
        {
            var dtoInvoice = request.Adapt<InvoiceDTO>();
            dtoInvoice.InvoiceStatus = Invoice.Status.INVOICE_APPROVED;
            repository.Set(dtoInvoice);
            return Task.CompletedTask;
        }

        public Task Handle(InvoiceCompleted request, CancellationToken cancellationToken)
        {
            var dtoInvoice = request.Adapt<InvoiceDTO>();
            dtoInvoice.InvoiceStatus = Invoice.Status.INVOICE_COMPLETED;
            repository.Set(dtoInvoice);
            return Task.CompletedTask;
        }
    }
}