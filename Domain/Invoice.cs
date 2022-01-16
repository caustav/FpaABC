using System;
using Domain.Common;
using Domain.DomainEvents;
using Microsoft.Extensions.Logging;

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

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string InvoiceNumber { get; set; } = string.Empty;
        public string InvoiceAmount { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public Status InvoiceStatus { get; private set; } = Status.INVOICE_UNKNOWN;

        private ILogger<Invoice>? Logger { get; set;}

        public new void AddLogger(ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
            this.Logger = LoggerFactory!.CreateLogger<Invoice>();
            base.AddLogger(loggerFactory);
        }

        public void Create(string invoiceNumber, string invoiceAmount)
        {
            ArgumentNullException.ThrowIfNull(InvoiceNumber);

            RaiseEvent<InvoiceCreated>((@event)=>
            {
                @event.InvoiceNumber = invoiceNumber;
                @event.InvoiceAmount = InvoiceAmount;
            });
        }

        public void Approve()
        {
            ArgumentNullException.ThrowIfNull(InvoiceNumber);
            RaiseEvent<InvoiceApproved>((@event)=>
            {
                @event.InvoiceNumber = InvoiceNumber;
            });
        }

        public void Complete()
        {
            ArgumentNullException.ThrowIfNull(InvoiceNumber);

            RaiseEvent<InvoiceCompleted>((@event)=>
            {
                @event.InvoiceNumber = InvoiceNumber;
                @event.DueDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            });
        }

        public void Apply(InvoiceCreated invoiceCreated)
        {
            InvoiceStatus = Status.INVOICE_PENDING;
            InvoiceAmount = invoiceCreated.InvoiceAmount;
            InvoiceNumber = invoiceCreated.InvoiceNumber;
        }   

        public void Apply(InvoiceApproved invoiceApproved)
        {
            InvoiceStatus = Status.INVOICE_APPROVED;
            InvoiceNumber = invoiceApproved.InvoiceNumber;
        }
        
        public void Apply(InvoiceCompleted invoiceCompleted)
        {
            InvoiceStatus = Status.INVOICE_COMPLETED;
            InvoiceNumber = invoiceCompleted.InvoiceNumber;
            Date = invoiceCompleted.DueDate;
        }
    }
}
