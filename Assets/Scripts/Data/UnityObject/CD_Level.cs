using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefense/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelDatas;
    }
}