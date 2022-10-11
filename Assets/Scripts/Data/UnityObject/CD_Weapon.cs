using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Weapon", menuName = "BaseDefense/CD_Weapon", order = 0)]
    public class CD_Weapon : ScriptableObject
    {
        public List<WeaponData> WeaponDatas = new List<WeaponData>();
    }
}