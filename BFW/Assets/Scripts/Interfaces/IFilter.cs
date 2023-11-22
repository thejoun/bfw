namespace Interfaces
{
    public interface IFilter<in T>
    {
        bool Accepts(T input);
    }
}