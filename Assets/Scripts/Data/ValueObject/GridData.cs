using System;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class GridData
    {
        public bool isDynamic;
        
        [ShowIf("isDynamic")]
        public StackerType StackerType;
        
        [HideIf("isDynamic")]
        public StackAreaType StackAreaType;
        
        
        [Title("Int")]
        [Tooltip("Row, Column and Depth Settings")]
        [MinValue(1)]
        public Vector3 GridSize;

        [Tooltip("Distance between two objects")]
        public Vector3 Offset; 
        
        [Tooltip("Choose which mesh will be drawned in the Editor Scene")]
        public Mesh DrawnedMesh;
    }
}