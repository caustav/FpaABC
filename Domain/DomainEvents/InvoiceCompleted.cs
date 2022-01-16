using Domain.Common;
using Newtonsoft.Json;

namespace Domain.DomainEvents
{
    public class InvoiceCompleted : DomainEvent, IConvertFrom<InvoiceCompleted>
    {
        public string InvoiceNumber {get; set;} = string.Empty;
        public string DueDate {get; set;} = string.Empty;
        public InvoiceCompleted ConvertFrom(string str)
        {
            var obj = JsonConvert.DeserializeObject<InvoiceCompleted>(str);
            ArgumentNullException.ThrowIfNull(obj);
            return obj;
        }
    }
}