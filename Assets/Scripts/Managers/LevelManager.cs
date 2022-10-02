using System;
using Abstract;
using Abstract.Interfaces;
using Commands.LevelCommands;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour,ISaveable
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public LevelData Data;

        #endregion

        #region Private Variables

        private int _levelID;
        
        private int _uniqueID;
        
        #endregion

        #region Serialized Variables

        [Space][SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoaderCommand;
        [SerializeField] private ClearActiveLevelCommand clearActiveLevelCommand;

        #endregion

        #endregion


        private void Awake()
        {
            GetData();
        }

        private void GetData()
        {
            if (!ES3.FileExists($"Level{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Level"))
                {
                    Data = GetLevelData();
                }
            }
        }
        
        private LevelData GetLevelData()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelDatas.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[newLevelData];
        }
         

        #region Event Subscribetions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnNextLevel()
        {
            _levelID++;
            Save(_levelID);
            CoreGameSignals.Instance.onReset?.Invoke();
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

        private void OnSave()
        {
            Save(_levelID);
        }

        private void OnLoad()
        {
            Load(_levelID);
        }
        public void Save(int uniqueId)
        {
            LevelData levelData = new LevelData();
            
            //SaveLoadSignals.Instance.
        }

        public void Load(int uniqueId)
        {
            
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
     
        }
    }
}
