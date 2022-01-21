namespace Application.EventSubscriber
{
    public interface ISubscriber
    {
        Task OnUpdate(string @eventStr);
    }
}