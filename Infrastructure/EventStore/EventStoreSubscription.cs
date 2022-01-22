using EventStore.Client;
using Application.EventSubscriber;
using System.Text;
using Microsoft.Extensions.Logging;
using Grpc.Core;

namespace Infrastructure.EventStore
{
    public class EventStoreSubscription : IEventStoreSubscription
    {
        ISubscriber subscriber;
        ILogger<EventStoreSubscription> logger;

        EventStorePersistentSubscriptionsClient? client;
        UserCredentials? userCredentials;

        public EventStoreSubscription(ISubscriber subscriber, ILogger<EventStoreSubscription> logger)
        {
            this.subscriber = subscriber;
            this.logger = logger;
        }
        
        public async Task Enable()
        {
            await CreateSubscription();
            await ConnectToSubscription();
        }

        private async Task CreateSubscription()
        {
            try
            {
                client = new EventStorePersistentSubscriptionsClient(
                    EventStoreClientSettings.Create("esdb://127.0.0.1:2111,127.0.0.1:2112,127.0.0.1:2113?tls=true&tlsVerifyCert=false")
                );

                this.userCredentials = new UserCredentials("admin", "changeit");

                var settings = new PersistentSubscriptionSettings(resolveLinkTos:true);
                await client.CreateAsync("$et-fpa-event", "fpa-group", settings, userCredentials);                
            }
            catch (RpcException e)
            {
                if (e.Status.StatusCode != Grpc.Core.StatusCode.AlreadyExists)
                {
                    throw new Exception("Unable to create subscription", e);
                }                
            }
            catch(System.InvalidOperationException e)
            {
                if (e.InnerException is not RpcException)
                {
                    throw new Exception("Unable to create subscription", e); 
                } 
                else if(e.InnerException is RpcException)
                {
                    var rpcException = e.InnerException as RpcException;
                    if (rpcException!.StatusCode != Grpc.Core.StatusCode.AlreadyExists)
                    {
                        throw new Exception("Unable to create subscription", e.InnerException);
                    }
                }
            }
        }

        private async Task ConnectToSubscription()
        {
            try
            {
                var subscription = await client!.SubscribeAsync(
                    "$et-fpa-event",
                    "fpa-group",
                    async (subscription, resolvedEvent, bufferSize, cancellationToken) => {
                        string str = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
                        await subscriber.OnUpdate(Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray()));
                    },
                    (subscription, reason, exception) => {
                        logger.LogError(exception!.Message);
                }, userCredentials);                  
            }
            catch (RpcException e)
            {
                throw new Exception("Unable to connect to subscription", e);
            }              
        }
    }
}