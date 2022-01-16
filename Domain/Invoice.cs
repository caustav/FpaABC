using System;
using Domain.Common;
using Domain.DomainEvents;

namespace Domain
{
    public class Invoice : Aggregate, ICanApply<InvoiceCreated>, 
                                        ICanApply<InvoiceApproved>, 
                                            ICanApply<InvoiceCompleted>
    {
        public enum Status
        {
            INVOICE_UNKNOWN,
            INVOICE_PENDING,
            INVOICE_APPROVED,
            INVOICE_COMPLETED
        }

        public string Id { get; set; } = string.Empty;
        public string InvoiceNumber { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public Status InvoiceStatus { get; private set; } = Status.INVOICE_UNKNOWN;

        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
            var @event = new InvoiceCreated();
            Apply(@event);
        }

        public void Approve()
        {
            var @event = new InvoiceApproved();
            Apply(@event);
        }

        public void Complete()
        {
            var @event = new InvoiceCompleted();
            Apply(@event);
        }

        public void Apply(InvoiceCreated invoiceCreated)
        {
            InvoiceStatus = Status.INVOICE_PENDING;
            Apply<InvoiceCreated>(invoiceCreated);
        }   

        public void Apply(InvoiceApproved invoiceApproved)
        {
            InvoiceStatus = Status.INVOICE_APPROVED;
            Apply<InvoiceApproved>(invoiceApproved);
        }
        
        public void Apply(InvoiceCompleted invoiceCompleted)
        {
            InvoiceStatus = Status.INVOICE_COMPLETED;
            Apply<InvoiceCompleted>(invoiceCompleted);
        }
    }
}
