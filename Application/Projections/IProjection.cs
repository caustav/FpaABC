namespace Application.Projections
{
    public interface IProjection
    {
        Task Handle(string @eventStr);
    }
}