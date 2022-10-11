using Commands.LevelCommands;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _levelID;
        
        #endregion

        #region Serialized Variables

        [Space][SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoaderCommand;
        [SerializeField] private ClearActiveLevelCommand clearActiveLevelCommand;

        #endregion

        #endregion
        
        #region Event Subscribetions
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadLevelID += OnLoadLevelID;
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadLevelID -= OnLoadLevelID;
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnNextLevel()
        {
            _levelID++;
            SaveLevelID(_levelID);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void SaveLevelID(int levelID)
        {
            InitializeDataSignals.Instance.onSaveLevelID?.Invoke(levelID);
        }

        private void OnLoadLevelID(int levelID)
        {
            _levelID = levelID;
        }
        
        private void OnInitializeLevel()
        {
            int newlevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelDatas.Count;
            levelLoaderCommand.InitializeLevel(newlevelData,levelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            clearActiveLevelCommand.ClearActiveLevel(levelHolder.transform);
        }
 
    }
}
