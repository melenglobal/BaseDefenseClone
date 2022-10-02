using UnityEngine;

namespace Abstract.Interfaces
{
    public interface ILevelLoader
    {
        void InitializeLevel(int _levelID,Transform levelHolder);
    }
}