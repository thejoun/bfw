namespace Unimorph.Core
{
    public interface IOptional<out T>
    {
        public bool HasCondition { get; }
        public T Value { get; }
    }
}