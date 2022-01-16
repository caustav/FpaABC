namespace Domain.Common
{
    public interface ICanApply<T>
    {
        void Apply(T t);
    }
}