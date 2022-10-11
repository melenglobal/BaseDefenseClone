using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> UIPanelsList = new List<GameObject>();        
        public void OpenPanel(UIPanels panelParams)
        {
            UIPanelsList[(int)panelParams].SetActive(true);
        }

        public  void ClosePanel(UIPanels panelParams)
        {
            UIPanelsList[(int)panelParams].SetActive(false);
        }
    }
}
