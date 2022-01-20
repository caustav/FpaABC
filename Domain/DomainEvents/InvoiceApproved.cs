using Domain.Common;
using Newtonsoft.Json;
using MediatR;

namespace Domain.DomainEvents
{
    public class InvoiceApproved : DomainEvent, IConvertFrom<InvoiceApproved>, INotification
    {
        public string InvoiceNumber {get; set;} = string.Empty;
        public InvoiceApproved ConvertFrom(string str)
        {
            var obj = JsonConvert.DeserializeObject<InvoiceApproved>(str);
            ArgumentNullException.ThrowIfNull(obj);
            return obj;
        }
    }
}