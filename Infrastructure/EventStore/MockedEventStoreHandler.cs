using Application.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

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
        private ILogger<MockedEventStoreHandler> Logger;
        
        public MockedEventStoreHandler(ILogger<MockedEventStoreHandler> logger)
        {
            this.Logger = logger;
        }

        public Task Publish<TEvent>(IEnumerable<TEvent> @domainEvents)
        {
            foreach (var item in domainEvents)
            {
                Logger.LogInformation(item!.ToString());            
            }

            return  Task.CompletedTask;
        }
        
        public IEnumerable<string> GetEvents(string aggregateId)
        {
            var e1 = new InvoiceCreated
            {
                Name = nameof(InvoiceCreated),
                InvoiceNumber = "I-980",
                InvoiceAmount = "2500 INR"
            };

            var e2 = new InvoiceApproved
            {
                Name = nameof(InvoiceApproved),
                InvoiceNumber = "I-980"
            };

            return new List<string>()
            {
                JsonConvert.SerializeObject(e1).ToString(),
                JsonConvert.SerializeObject(e2).ToString()
            };
        }
    }
}