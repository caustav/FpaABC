using Application.Common;
using Newtonsoft.Json;

namespace Infrastructure.EventStore
{
    class InvoiceCompleted 
    {
        public string Name {get; set;} = string.Empty;
        public string InvoiceNumber {get; set;} = string.Empty;
        public string DueDate {get; set;} = string.Empty;
    }

    class InvoiceApproved 
    {
        public string Name {get; set;} = string.Empty;
        public string InvoiceNumber { get; set; } = string.Empty;
    }

    class InvoiceCreated 
    {
        public string Name {get; set;} = string.Empty;
        public string InvoiceNumber {get; set;} = string.Empty;
        public string InvoiceAmount {get; set;} = string.Empty;
    }

    public class MockedEventStoreHandler : IEventStoreHandler
    {
        public Task Publish<TEvent>(IEnumerable<TEvent> @domainEvents)
        {
            /// DO NOTHING
            return  Task.CompletedTask;
        }
        public IEnumerable<string> GetEvents(string aggregateId)
        {
            var e1 = new InvoiceCreated
            {
                InvoiceNumber = "I-980",
                InvoiceAmount = "2500 INR"
            };

            var e2 = new InvoiceApproved
            {
                InvoiceNumber = "I-980"
            };

            var e3 = new InvoiceCompleted
            {
                InvoiceNumber = "I-980",
                DueDate = "20220116"
            };

            return new List<string>()
            {
                JsonConvert.SerializeObject(e1).ToString(),
                JsonConvert.SerializeObject(e2).ToString(),
                JsonConvert.SerializeObject(e3).ToString()
            };
        }
    }
}