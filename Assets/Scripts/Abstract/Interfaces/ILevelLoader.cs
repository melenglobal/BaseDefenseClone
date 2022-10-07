using UnityEngine;

namespace Abstract.Interfaces
{
    public interface ILevelLoader
    {
        void InitializeLevel(int levelID,Transform levelHolder);
    }
}