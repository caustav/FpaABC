using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class MongoRepositoryInvoice : IRepository<InvoiceDTO>
    {
        private const string CollectionName = "Invoice";

        public MongoAdapter MongoAdapter { get; private set; }

        public MongoRepositoryInvoice(MongoAdapter mongoAdapter)
        {
            this.MongoAdapter = mongoAdapter;
        }

        public InvoiceDTO Add(InvoiceDTO invoiceObj)
        {
            MongoAdapter.Insert(CollectionName, invoiceObj);
            return invoiceObj;
        }

        public InvoiceDTO Delete(InvoiceDTO invoiceObj)
        {
            throw new NotImplementedException();
        }

        public InvoiceDTO Get(InvoiceDTO invoiceObj)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDTO> Get(string id)
        {
            return await MongoAdapter.Read<InvoiceDTO>(CollectionName, "{" + $"\"InvoiceNumber\":\"{id}\"" + "}");
        }

        public IEnumerable<InvoiceDTO> GetAll()
        {
            return MongoAdapter.ReadAll<InvoiceDTO>(CollectionName);
        }

        public async Task Set(InvoiceDTO invoiceObj)
        {
            await MongoAdapter.Update(CollectionName, 
                invoiceObj, "{" + $"\"InvoiceNumber\":\"{invoiceObj.InvoiceNumber}\"" + "}");
        }
    }
}
