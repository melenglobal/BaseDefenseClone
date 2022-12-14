using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class DataInitManager : MonoBehaviour,ISaveable
    {
        #region Self Variables
    
        #region Public Variables

        #endregion
    
        #region Serialized Variables
    
        [SerializeField] 
        private List<LevelData> levelDatas = new List<LevelData>();
    
        [SerializeField] 
        private CD_Level cdLevel;
        

        #endregion
    
        #region Private Variables
    
        private int _levelID;
        private int _uniqueID = 12;
    
        private LevelData _levelData;
        private BaseRoomData _baseRoomData;
        private MineBaseData _mineBaseData;
        private MilitaryBaseData _militaryBaseData;
        private BuyablesData _buyablesData;
        private ScoreData _scoreData;

        #endregion
    
        #endregion
        
        #region Initialize Scriptable Data
        private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");

        private void Awake()
        {
            cdLevel = GetLevelDatas();
            _levelID = cdLevel.LevelId;
            _scoreData = cdLevel.ScoreData;
            levelDatas = cdLevel.LevelDatas;
        }

        private void Start()
        {   
            InitData();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void InitData()
        {
            if (!ES3.FileExists($"LevelData{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("LevelData"))
                {   
                    cdLevel = GetLevelDatas();
                    _levelID = cdLevel.LevelId;
                    levelDatas=cdLevel.LevelDatas;
                    _scoreData = cdLevel.ScoreData;
                    Save(_uniqueID);
                }
            }
            Load(_uniqueID);
        }

        #endregion

        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InitializeDataSignals.Instance.onSaveLevelID += OnSyncLevelID;
            CoreGameSignals.Instance.onLevelInitialize += OnSyncLevel;
            InitializeDataSignals.Instance.onSaveBaseRoomData += OnSyncBaseRoomDatas;
            InitializeDataSignals.Instance.onSaveMineBaseData += OnSyncMineBaseDatas;
            InitializeDataSignals.Instance.onSaveMilitaryBaseData += OnSyncMilitaryBaseData;
            InitializeDataSignals.Instance.onSaveBuyablesData += OnSyncBuyablesData;
            InitializeDataSignals.Instance.onSaveGameScore += OnSyncScoreData;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;

            InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
            InitializeDataSignals.Instance.onLoadBaseRoomData += OnLoadBaseRoomData;
            InitializeDataSignals.Instance.onLoadBuyablesData += OnLoadBuyablesData;
            InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;
            InitializeDataSignals.Instance.onLoadGameScore += OnLoadScoreData;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onApplicationPause += OnSave;

        }

        private void UnsubscribeEvents()
        {
            InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
            CoreGameSignals.Instance.onLevelInitialize -= OnSyncLevel;
            InitializeDataSignals.Instance.onSaveBaseRoomData -= OnSyncBaseRoomDatas;
            InitializeDataSignals.Instance.onSaveMineBaseData -= OnSyncMineBaseDatas;
            InitializeDataSignals.Instance.onSaveMilitaryBaseData -= OnSyncMilitaryBaseData;
            InitializeDataSignals.Instance.onSaveBuyablesData -= OnSyncBuyablesData;
            InitializeDataSignals.Instance.onSaveGameScore -= OnSyncScoreData;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoad;
            
        
            InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
            InitializeDataSignals.Instance.onLoadBaseRoomData -= OnLoadBaseRoomData;
            InitializeDataSignals.Instance.onLoadBuyablesData -= OnLoadBuyablesData;
            InitializeDataSignals.Instance.onLoadMineBaseData -= OnLoadMineBaseData;
            InitializeDataSignals.Instance.onLoadGameScore -= OnLoadScoreData;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onPreNextLevel -= OnSave;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            
            
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion
    
        #region Send Data to Managers
        private void SendDataManagers() => InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
        private ScoreData OnLoadScoreData() => _scoreData;
        private MilitaryBaseData OnLoadMilitaryBaseData() =>_militaryBaseData;
        private BaseRoomData OnLoadBaseRoomData() => _baseRoomData;
        private MineBaseData OnLoadMineBaseData() =>_mineBaseData;
        private BuyablesData OnLoadBuyablesData() => _buyablesData;
    
        #endregion
    
        #region Level Save - Load

        private void OnSave() => Save(_uniqueID);

        private void OnLoad() => Load(_uniqueID);
        
        public void Save(int uniqueId)
        {
            cdLevel = new CD_Level(_levelID,levelDatas,_scoreData);
        
            SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel,uniqueId);
        
        }
    
        public void Load(int uniqueId)
        {
            CD_Level cdLevel = SaveLoadSignals.Instance.onLoadGameData.Invoke(this.cdLevel.GetKey(), uniqueId);
            _levelID = cdLevel.LevelId;
            levelDatas = cdLevel.LevelDatas;
            _scoreData = cdLevel.ScoreData;
            _baseRoomData = cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData;
            _mineBaseData = cdLevel.LevelDatas[_levelID].BaseData.MineBaseData;
            _militaryBaseData = cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData;
            _buyablesData = cdLevel.LevelDatas[_levelID].BaseData.BuyablesData;

        }

        #endregion
    
        #region Get Data from Managers
        private void OnSyncLevel() => SendDataManagers();
        private void OnSyncLevelID(int levelID) => _levelID= levelID;

        private void OnSyncScoreData(ScoreData scoreData) => _scoreData = scoreData;

        private void OnSyncBaseRoomDatas(BaseRoomData baseRoomData) => _baseRoomData = baseRoomData;

        private void OnSyncMineBaseDatas(MineBaseData mineBaseData) => _mineBaseData = mineBaseData;

        private void OnSyncMilitaryBaseData(MilitaryBaseData militaryBaseData) => _militaryBaseData= militaryBaseData;

        private void OnSyncBuyablesData(BuyablesData buyablesData) => _buyablesData = buyablesData;

        #endregion
   
    }
}
