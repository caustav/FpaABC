namespace Infrastructure.EventStore
{
    public interface IEventStoreSubscription
    {
         Task Enable();
    }
}