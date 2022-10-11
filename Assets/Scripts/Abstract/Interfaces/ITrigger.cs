using UnityEngine;

namespace Abstract.Interfaces
{
    public interface ITriggerEnter
    { 
        GameObject TriggerEnter();
    }

    public interface ITriggerExit
    {
        void TriggerExit();
    }

    public interface ITriggerStay
    {
        void TriggerStay();
    }
}