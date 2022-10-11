using Abstract.Interfaces;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace Commands.LevelCommands
{
    public class LevelLoaderCommand : MonoBehaviour,ILevelLoader
    {
        public void InitializeLevel(int levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"LevelPrefabs/Level{levelID}"), levelHolder);
        }
    }
}