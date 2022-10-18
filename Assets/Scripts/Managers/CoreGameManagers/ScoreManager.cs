using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class ScoreManager : MonoBehaviour
    {
        private ScoreData _scoreData;

        private int _paymentAmount = -10;

        private int _earningAmount = 10;

        private bool isStop { get; set; }

        private void OnGetData()
        {
            _scoreData = InitializeDataSignals.Instance.onLoadGameScore.Invoke();
            SetGameScore();
        } 

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnGetData;
            CoreGameSignals.Instance.onHasEnoughMoney += OnHasMoney;
            CoreGameSignals.Instance.onHasEnoughGem += OnHasGem;
            CoreGameSignals.Instance.onUpdateGemScoreData += OnUpdateGemScore;
            CoreGameSignals.Instance.onUpdateMoneyScoreData += OnUpdateMoneyScore;
            CoreGameSignals.Instance.onStartMoneyPayment += OnStartMoneyPayment;
            CoreGameSignals.Instance.onStopMoneyPayment += OnStopMoneyPayment;
            CoreGameSignals.Instance.onPreNextLevel += OnPreNextLevel;
        }
        private void UnsubscribeEvents()
        {   
            CoreGameSignals.Instance.onLevelInitialize -= OnGetData;
            CoreGameSignals.Instance.onHasEnoughMoney -= OnHasMoney;
            CoreGameSignals.Instance.onHasEnoughGem -= OnHasGem;
            CoreGameSignals.Instance.onUpdateGemScoreData -= OnUpdateGemScore;
            CoreGameSignals.Instance.onUpdateMoneyScoreData -= OnUpdateMoneyScore;
            CoreGameSignals.Instance.onStartMoneyPayment -= OnStartMoneyPayment;
            CoreGameSignals.Instance.onStopMoneyPayment -= OnStopMoneyPayment;
            CoreGameSignals.Instance.onPreNextLevel -= OnPreNextLevel;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private bool OnHasMoney() =>  _scoreData.TotalMoneyScore !=0;

        private bool OnHasGem() => _scoreData.TotalGemScore != 0;
        private void SetGameScore()
        {
            UISignals.Instance.onUpdateMoneyScore?.Invoke(_scoreData.TotalMoneyScore);
            UISignals.Instance.onUpdateGemScore?.Invoke(_scoreData.TotalGemScore);
        }

        private void OnStartMoneyPayment()
        {
            UpdateMoneyScore(_paymentAmount);
        }
        

        private void OnStopMoneyPayment()
        {
            isStop = true;
        }

        private void OnUpdateGemScore()
        {
            UpdateGemScore(_earningAmount);
        }

        private void OnUpdateMoneyScore()
        {
            UpdateMoneyScore(_earningAmount);
        }
        private void UpdateMoneyScore(int _amount)
        {
            _scoreData.TotalMoneyScore += _amount;
            UISignals.Instance.onUpdateMoneyScore?.Invoke(_scoreData.TotalMoneyScore);
            UpdateGameScoreData();
        }

        private void UpdateGemScore(int _amount)
        {
            _scoreData.TotalGemScore += _amount;
            UISignals.Instance.onUpdateGemScore?.Invoke(_scoreData.TotalGemScore);
            UpdateGameScoreData();
        }

        private void UpdateGameScoreData() => InitializeDataSignals.Instance.onSaveGameScore?.Invoke(_scoreData);

        private void OnPreNextLevel()
        {
            InitializeDataSignals.Instance.onSaveGameScore?.Invoke(_scoreData);
        }
    }
}