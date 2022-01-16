using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Repositories
{
    public class ConsoleRepository : IRepository<InvoiceDTO>
    {
        public InvoiceDTO Add(InvoiceDTO invoice)
        {
            Console.WriteLine(JsonSerializer.Serialize(invoice));
            return invoice;
        }

        public InvoiceDTO Delete(InvoiceDTO invoice)
        {
            throw new NotImplementedException();
        }

        public InvoiceDTO Get(InvoiceDTO invoice)
        {
            return new InvoiceDTO { InvoiceNumber = "I#1234",   CompanyId = "1234", Vendor = "V-123"};
        }

        public InvoiceDTO Get(string id)
        {
            return new InvoiceDTO { InvoiceNumber = "II#1234", CompanyId = "1234", Vendor = "V-123" };
        }

        public IEnumerable<InvoiceDTO> GetAll()
        {
            return new List<InvoiceDTO>()
            {
                new InvoiceDTO { InvoiceNumber = "II#1234", CompanyId = "1234", Vendor = "V-123" },
                new InvoiceDTO { InvoiceNumber = "I#1234", CompanyId = "1234", Vendor = "V-123" }
            };
        }

        public InvoiceDTO Set(InvoiceDTO invoice)
        {
            return new InvoiceDTO { InvoiceNumber = "IIS#1234", CompanyId = "1234", Vendor = "V-1234" };
        }
    }
}
