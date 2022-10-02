using Abstract.Interfaces;
using UnityEngine;

namespace Commands.LevelCommands
{
    public class ClearActiveLevelCommand : MonoBehaviour,ILevelClearer
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            Destroy(levelHolder.GetChild(0).gameObject);
            //ReleasePool
        }
    }
}