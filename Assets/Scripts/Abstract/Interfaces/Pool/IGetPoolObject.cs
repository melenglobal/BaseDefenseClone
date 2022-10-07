using Enums;
using UnityEngine;

namespace Abstract.Interfaces.Pool
{
    public interface IGetPoolObject
    {
        GameObject GetObject(PoolType poolName);
    }
}
