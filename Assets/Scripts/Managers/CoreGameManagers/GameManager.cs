using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
        private void OnApplicationPause(bool IsPaused)
        {
            if (IsPaused) CoreGameSignals.Instance.onApplicationPause?.Invoke();
        }
      
        private void OnApplicationQuit()
        {
            CoreGameSignals.Instance.onApplicationQuit?.Invoke();
        }

    }
}