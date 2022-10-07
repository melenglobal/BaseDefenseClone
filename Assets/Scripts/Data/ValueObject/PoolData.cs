using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Data.ValueObject
{
    [Serializable]
    public class PoolData
    {
        public GameObject ObjectType;
        public Transform PoolHolder;
        public int initalAmount;
        public bool isDynamic;
    } 
}
