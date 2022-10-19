using UnityEngine;

namespace Abstract.Interfaces
{
    public interface ICustomer
    {
        bool CanPay { get; set; }

        void PlayPaymentAnimation(Transform transform);

    }
}