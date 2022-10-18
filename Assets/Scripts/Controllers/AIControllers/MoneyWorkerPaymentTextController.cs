using TMPro;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class MoneyWorkerPaymentTextController : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshPro remainingCostText;

        public void SetInitText(int cost) => remainingCostText.text = cost.ToString();

        public void UpdateText(int cost) => remainingCostText.text = cost.ToString();
    }
}