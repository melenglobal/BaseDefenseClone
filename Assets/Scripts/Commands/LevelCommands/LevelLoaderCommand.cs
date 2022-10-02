using Abstract.Interfaces;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace Commands.LevelCommands
{
    public class LevelLoaderCommand : MonoBehaviour,ILevelLoader
    {
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
            //Get From Pool
        }
    }
}