using System.Numerics;
using UnityEngine;

namespace Interfaces
{
    public interface IEntity : IKeyed<BigInteger>, IKeyed<int>
    {
        BigInteger Id { get; }
        GameObject GameObject { get; }

        bool TryGetComponent<T>(out T component);

        BigInteger IKeyed<BigInteger>.Key => Id;
        int IKeyed<int>.Key => (int)Id;
    }

    public interface IHasEntity : IEntity
    {
        IEntity Entity { get; }

        BigInteger IEntity.Id => Entity.Id;
        GameObject IEntity.GameObject => Entity.GameObject;

        bool IEntity.TryGetComponent<T>(out T component) => Entity.TryGetComponent(out component);
    }
}