using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class RoomPaymentTextController : MonoBehaviour
    {
        
        [SerializeField] 
        private TextMeshPro remainingCostText;

        public void SetInitText(int cost) => remainingCostText.text = cost.ToString();

        public void UpdateText(int cost) => remainingCostText.text = cost.ToString();
    }
}