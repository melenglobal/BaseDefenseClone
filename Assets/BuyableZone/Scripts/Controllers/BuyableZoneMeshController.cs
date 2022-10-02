using Buyablezone.Scripts;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Buyablezone
{
    public class BuyableZoneMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Header("TextBounceEffect")]
        [SerializeField]
        private float bounceValue=0.2f;
        [SerializeField]
        private float bounceDuration=0.3f;
        [Header("References")]
        [SerializeField]
        private SpriteRenderer radialIcon;
        [SerializeField]
        private SpriteRenderer buyableZoneSprite;
        [SerializeField] private  TextMeshPro buyableZoneText;

        [Header("BuyableZone State Icons")] 
        [SerializeField]
        private GameObject CoolDownIcon;
        [SerializeField]
        private GameObject RequiredItemIcon;


        #endregion
        

        #endregion
                
      public void TextBounceEffectActive(bool isActive)
      {
          float _currentBounceValue=(isActive)?bounceValue:-bounceValue;
          buyableZoneSprite.transform.DOScale(
              buyableZoneSprite.transform.localScale+(Vector3.one * _currentBounceValue)
              ,duration:bounceDuration);

      }
      public void RadialProgress(float initialTimer,float initialTimeOffset)
      {
          float currentRatio=CalculateRadialRatio(initialTimer,initialTimeOffset);
          radialIcon.material.SetFloat("_Arc1", currentRatio);

      }

      private float CalculateRadialRatio(float initialTimer,float initialTimeOffset)
      {
          return initialTimer / initialTimeOffset * 360;
      }
      public void UpdateDropzoneText(int value)
      {
         
          buyableZoneText.text = value.ToString();
      }
      public void UpdateDropzoneIcon(BuyableZoneIconType iconType)
            {
                switch (iconType)
                {
                    case BuyableZoneIconType.CoolDown:
                        RequiredItemIcon.SetActive(false);
                        CoolDownIcon.SetActive(true);
                        break;
                    case BuyableZoneIconType.RequiredAmount:
                        CoolDownIcon.SetActive(false);
                        RequiredItemIcon.SetActive(true);
                        break;
                }
            }
      public void BuyableScoreTextActive(bool isActive)
      {
          gameObject.SetActive(isActive);
      }

      public void StartPaymentFailedAnimation()
      {
          var _initialColor=buyableZoneText.color;
          buyableZoneText.color=Color.red;
          transform.DOPunchPosition(new Vector3(.2f,0,0), 0.1f, 1, 1).OnComplete(() =>
          {
              buyableZoneText.color=_initialColor;
          } );

      }
    }
}