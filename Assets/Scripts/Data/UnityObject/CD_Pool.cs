using UnityEngine;
using UnityEngine.Rendering;
using Enums;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "BaseDefence/CD_Pool",
        order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public SerializedDictionary<PoolType,PoolData> PoolDataDic = new SerializedDictionary<PoolType,PoolData>();
    }
}
