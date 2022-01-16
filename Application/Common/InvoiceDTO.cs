using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Invoice;

namespace Application.Common
{
    public class InvoiceDTO
    {
        public string Id { get; set; }  = default!;
        public string InvoiceNumber { get; set; }  = default!;
        public string Date { get; set; }  = default!;
        public string CompanyId { get; set; }  = default!;
        public string Vendor { get; set; }  = default!;

        public Status InvoiceStatus { get; private set; }
    }
}
