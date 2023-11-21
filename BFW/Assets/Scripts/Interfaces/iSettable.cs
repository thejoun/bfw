namespace Interfaces
{
    public interface ISettable<in T>
    {
        void SetValue(T value);
    }
}