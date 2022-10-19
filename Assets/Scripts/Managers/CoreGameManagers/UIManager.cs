using System;
using Controllers;
using Controllers.BaseControllers;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.CoreGameManagers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] 
        private UIPanelController uiPanelController;
        
        [SerializeField] 
        private TextMeshProUGUI gemScore;
        
        [SerializeField] 
        private TextMeshProUGUI moneyScore;
        
        [SerializeField] 
        private TextMeshProUGUI levelText;

        [SerializeField] 
        private TextMeshProUGUI healthText;
        
        [SerializeField] 
        private Scrollbar healthBar;

        #endregion SerializeField Variables

        #region Private Variables

        private const int _percentage = 100;
        

        #endregion

        #endregion Self Veriables

        private void Start()
        {
            OnGetHealthValue();
        }

        #region Event Subcriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore += OnUpdateGemScore;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            UISignals.Instance.onHealthUpdate += OnHealthUpdate;
            UISignals.Instance.onHealthBarClose += OnHealthBarClose;
            UISignals.Instance.onHealthBarOpen += OnHealthBarOpen;
            CoreGameSignals.Instance.onPreNextLevel += OnPreNextLevel;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            
        }

        private void UnsubscribeEvents()
        {   
            UISignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            UISignals.Instance.onHealthUpdate -= OnHealthUpdate;
            UISignals.Instance.onHealthBarClose -= OnHealthBarClose;
            UISignals.Instance.onHealthBarOpen -= OnHealthBarOpen;
            CoreGameSignals.Instance.onPreNextLevel -= OnPreNextLevel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subcriptions
        
        private void OnOpenPanel(UIPanels panels) => uiPanelController.OpenPanel(panels);

        private void OnClosePanel(UIPanels panels) => uiPanelController.ClosePanel(panels);
   
        public void Play() =>  CoreGameSignals.Instance.onPlay?.Invoke();
        
        public void NextLevel() => CoreGameSignals.Instance.onNextLevel?.Invoke();

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.NextLevel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnPreNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.NextLevel);
        }

        private void OnHealthBarOpen() => UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayerHealthPanel);

        private void OnHealthBarClose() =>  UISignals.Instance.onClosePanel?.Invoke(UIPanels.PlayerHealthPanel);

        private void OnUpdateMoneyScore(int score) => moneyScore.text = score.ToString();

        private void OnUpdateGemScore(int score) =>  gemScore.text = score.ToString();
        
        private void OnSetHealthText(int healthValue) => healthText.text = healthValue.ToString();
        
        private void OnSetLevelText(int nextLevel)
        {
            nextLevel++;
            levelText.text = "Base " + nextLevel;
        }

        private void OnGetHealthValue() => OnHealthUpdate(CoreGameSignals.Instance.onGetHealthValue.Invoke());
        
        private void OnHealthUpdate(int healthValue)
        {
            OnSetHealthText(healthValue);
            healthBar.size = (float)healthValue/_percentage;
        }

    }
}
