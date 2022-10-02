
using System;
using System.Collections;
using Buyablezone;
using Buyablezone.ConditionHandlers;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Buyablezone.Scripts;
//using Signals;
using UnityEngine;

namespace Managers
{
    public class BuyableZoneManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        public bool IsReusable { get=>isReusable; }

        [Header("BuyableZone Settings")]
        [SerializeField]
        private bool isReusable;
        [SerializeField]
        private bool waitInputForInteraction;
        public bool coolDown;        
        [SerializeField] 
        private bool hasInitialTime;

        [Header("Time Settings")] 
        [SerializeField]
        private float PayOffset;
        [SerializeField]
        private int coolDownTime;
        [SerializeField]
        private float InitialOffset;

        [Header("References")]
        [SerializeField] private BuyableZoneMeshController buyableZoneMeshController;
        public IBuyable IBuyable { get; private set; }

        #endregion
        #endregion

        #region Handlers
            InitialTimerHandler InitialTimerHandler = new InitialTimerHandler();
            CheckPayOffsetHandler CheckPayOffsetHandler = new CheckPayOffsetHandler();
            CheckCanBuyHandler CheckCanBuyHandler = new CheckCanBuyHandler();
            CheckCanIncreaseAmountHandler CheckCanIncreaseAmountHandler = new CheckCanIncreaseAmountHandler();
            private BuyableZoneData _buyableZoneData;
            public PurchaseParam Purchase;
        #endregion

        private void Awake()
        {
            IBuyable = gameObject.GetComponentInParent<IBuyable>();
           
        }

        private void Start()
        {
          try
          {
              Purchase = new PurchaseParam(IBuyable.GetBuyableData(), PayOffset, InitialOffset, this);
             
          }
          catch (Exception e)
          {
              Debug.LogError("Interface not Implemented to Buyable Item");
              throw;
          }

          try
          {
              DefineTransitions();
              _buyableZoneData = Purchase.BuyableZoneList.BuyableZoneList[Purchase.CurrentBuyableLevel];
              UpdateDropzoneText(_buyableZoneData.RequiredAmount - _buyableZoneData.PayedAmount);
          }
          catch
          {
              Debug.LogError("BuyableZoneDataList or BuyableZoneData not Created");
              throw;
          }
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //InputSignals.Instance.onInputTakenActive += OnInputActive;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            //InputSignals.Instance.onInputTakenActive-= OnInputActive;
        }

        #endregion

        private void DefineTransitions()
        {
            InitialTimerHandler.SetSuccesor(CheckPayOffsetHandler);
            CheckPayOffsetHandler.SetSuccesor(CheckCanIncreaseAmountHandler);
            CheckCanIncreaseAmountHandler.SetSuccesor(CheckCanBuyHandler);
            
        } 
        private void OnInputActive(bool isActive)
        {
            /// Use This signal instead of InputController(); to Check input State
            //Purchase.isInputActive = (waitInputForInteraction) ? isActive : false;
        }
         private void Update()
                {
                    InputController();
                }
        
         public void InputController()
                {
                    if (Input.touchCount > 0||Input.GetMouseButtonDown(0))
                    {
                        Purchase.isInputActive = true;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        Purchase.isInputActive = false;
                    }
                }

        public void StartRadialProgress(float initialTimer,float initialTimeOffset)=>buyableZoneMeshController.RadialProgress(initialTimer,initialTimeOffset);
        public void StartButtonEvent()
        {
            if (coolDown&&!Purchase.isCoolDownCompleted)
            {
                return;
            }
            if (!Purchase.isInputActive)
                {
                    if (hasInitialTime&&!Purchase.alreadyBuyed)
                    {
                
                        InitialTimerHandler.ProcessRequest(Purchase);
                    }
                    else if(!hasInitialTime&&!Purchase.alreadyBuyed)
                    {
                        CheckPayOffsetHandler.ProcessRequest(Purchase);
                    }
                }


        }
        public void TextBounceEffectActive(bool isActive)=>buyableZoneMeshController.TextBounceEffectActive(isActive);
        public void UpdateDropzoneText(int Textvalue)=>buyableZoneMeshController.UpdateDropzoneText(Textvalue);

        public void UpdateDropzoneIcon(BuyableZoneIconType iconType) => buyableZoneMeshController.UpdateDropzoneIcon(iconType);
        public void BuyableScoreTextActive(bool isActive)=> buyableZoneMeshController.BuyableScoreTextActive(isActive);

        private IEnumerator CoolDownCount()
        {
            
            WaitForSeconds wait = new WaitForSeconds(coolDownTime);
            int counter = coolDownTime;
            while (counter > 0) {
                yield return new WaitForSeconds (1);
                counter--;
                buyableZoneMeshController.UpdateDropzoneText(counter);
            }
            _buyableZoneData = Purchase.BuyableZoneList.BuyableZoneList[Purchase.CurrentBuyableLevel];
            UpdateDropzoneText(_buyableZoneData.RequiredAmount - _buyableZoneData.PayedAmount);
            UpdateDropzoneIcon(BuyableZoneIconType.RequiredAmount);
            
            Purchase.isCoolDownCompleted = true;
        }

        public void StartCoolDownCount()
        {
            StartCoroutine("CoolDownCount");
        }

        #region Purchase Completion Configuration

        public void StartPurchaseConfiguration(PurchaseParam purchase)
        {
            PurchaseCompletedDataConfiguration(purchase);
            purchase.BuyableZoneManager.IBuyable.TriggerBuyingEvent();
            CheckIfReusable(purchase);
            CheckCoolDownCondition(purchase);
        } 
        private void PurchaseCompletedDataConfiguration(PurchaseParam purchase)
        { 
            purchase.InitialTimer = 0;
            purchase.BuyableZoneData.PayedAmount=0;
            purchase.isCoolDownCompleted = false;
        }

        private void CheckCoolDownCondition(PurchaseParam purchase)
        {
            if (!coolDown)
            {
                UpdateDropzoneText(purchase.BuyableZoneData.RequiredAmount - purchase.BuyableZoneData.PayedAmount);
                UpdateDropzoneIcon(BuyableZoneIconType.RequiredAmount);
            }
            else
            {
                UpdateDropzoneIcon(BuyableZoneIconType.CoolDown);
                StartCoolDownCount();
            }
        }

        private void CheckIfReusable(PurchaseParam purchase)
        {
            if (IsReusable)
            {
                purchase.IncreaseBuyableZoneLevel();
            }
            else
            {
                purchase.alreadyBuyed = true;
                BuyableScoreTextActive(false);
            }
        }

        #endregion

        public void StartPaymentFailed()
        {
            buyableZoneMeshController.StartPaymentFailedAnimation();
        }
       
    }

 }