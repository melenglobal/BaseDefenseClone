using Enums;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController UIPanelController;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private TextMeshProUGUI textMeshPro2;
        [SerializeField] private TextMeshProUGUI levelText;


        #endregion SerializeField Variables

        #endregion Self Veriables

        #region Event Subcription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onMultiplyArea += OnMultiplyArea;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onMultiplyArea -= OnMultiplyArea;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subcription
        

        private void OnOpenPanel(UIPanels panels)
        {
            UIPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(UIPanels panels)
        {
            UIPanelController.ClosePanel(panels);
        }
        
        public void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        
        public void OnMultiplyArea()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
      
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
        }
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void TryAgain()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void EnterIdleArea()
        {
            CoreGameSignals.Instance.onEnterIdleArea();
        }

        public void NextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
           
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        
        private void OnUpdateCurrentScore(int score)
        {
            textMeshPro.text = score.ToString();
            textMeshPro2.text = score.ToString();
        }

        private void OnSetLevelText(int nextLevel)
        {
            nextLevel++;
            levelText.text = "Level " + nextLevel;
        }

    }
}
