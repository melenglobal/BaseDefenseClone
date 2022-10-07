using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_TurretData", menuName = "Data/TurretData")]
    public class CD_TurretData : ScriptableObject
    {
        public TurretData turretDatas;
    }
}