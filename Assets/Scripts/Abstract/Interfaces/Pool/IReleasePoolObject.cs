using Enums;
using UnityEngine;

namespace Abstract.Interfaces.Pool
{
    public interface IReleasePoolObject
    {
        void ReleaseObject(GameObject obj, PoolType poolName);
    } 
}
