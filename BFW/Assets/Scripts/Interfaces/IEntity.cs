using System.Numerics;
using UnityEngine;

namespace Interfaces
{
    public interface IEntity
    {
        BigInteger Id { get; }
        GameObject GameObject { get; }
    }
}