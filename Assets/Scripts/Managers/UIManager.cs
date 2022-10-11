using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private TextMeshProUGUI gemScore; 
        [SerializeField] private TextMeshProUGUI moneyScore;
        [SerializeField] private TextMeshProUGUI levelText;
        
        #endregion SerializeField Variables

        #endregion Self Veriables

        #region Event Subcriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore += OnUpdateGemScore;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {   
            UISignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subcriptions
        
        private void OnOpenPanel(UIPanels panels) => uiPanelController.OpenPanel(panels);

        private void OnClosePanel(UIPanels panels) => uiPanelController.ClosePanel(panels);
   
        public void Play() =>  CoreGameSignals.Instance.onPlay?.Invoke();

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        public void OnNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        private void OnUpdateMoneyScore(int score)
        {
            moneyScore.text = score.ToString();
        }
        
        private void OnUpdateGemScore(int score) =>  gemScore.text = score.ToString();

        private void OnSetLevelText(int nextLevel)
        {
            nextLevel++;
            levelText.text = "Level " + nextLevel;
        }
        

    }
}
