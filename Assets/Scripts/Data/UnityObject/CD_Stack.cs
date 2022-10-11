using System.ComponentModel;
using Data.ValueObject;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Collections;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "StackSystem/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData[] StackDatas = new StackData[2];
    }
}