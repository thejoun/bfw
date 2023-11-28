using Zenject;

namespace Extensions
{
    public static class DiContainerExtensions
    {
        public static ConcreteIdBinderNonGeneric Bind<T1, T2>(this DiContainer container)
        {
            return container.Bind(typeof(T1), typeof(T2));
        }
    }
}