using Domain.Common;
using Newtonsoft.Json;
using System;

namespace Domain.DomainEvents
{
    public class InvoiceCreated : DomainEvent, IConvertFrom<InvoiceCreated>
    {
        public string InvoiceNumber {get; set;} = string.Empty;
        public string InvoiceAmount {get; set;} = string.Empty;
        public InvoiceCreated ConvertFrom(string str)
        {
            var obj = JsonConvert.DeserializeObject<InvoiceCreated>(str);
            ArgumentNullException.ThrowIfNull(obj);
            return obj;
        }
    }
}