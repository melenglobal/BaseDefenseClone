using System;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class StackData
    {
        public StackingSystem StackingSystem;
        
        [ShowIf("StackingSystem",Enums.StackingSystem.Static)]
        public List<GridData> StaticGridDatas;
        
        [ShowIf("StackingSystem",Enums.StackingSystem.Dynamic)]
        public List<GridData> DynamicGridDatas;
        
    }
}