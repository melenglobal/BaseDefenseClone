using System;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseExtentionController : MonoBehaviour 
    {
        private List<GameObject> OpenUpExtentions;
        private List<GameObject> CloseDownExtentions;
        
        public void ChangeExtentionVisibility(RoomTypes roomTypes)
        {
            OpenUpExtentions[(int)roomTypes].SetActive(true);
            CloseDownExtentions[(int)roomTypes].SetActive(false);
        }
    }
    
}