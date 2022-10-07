using System.Collections.Generic;
using Abstract.Stack;
using Controllers.AIStackerControllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.AIStackControllers
{
    public class AIMoneyStackController : AStack
    {
         #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform moneyTransformParent;
        
        [SerializeField] private StackingSystem stackingSystem;

        [ShowIf("stackingSystem", Enums.StackingSystem.Static)] 
        [SerializeField] private StackAreaType stackAreaType;
        
       
        [ShowIf("stackingSystem",Enums.StackingSystem.Static)]
        [SerializeField] private GridData stackAreaGridData;
    
        [ShowIf("stackingSystem",Enums.StackingSystem.Dynamic)]
        [SerializeField] private StackerType stackerType;
        
        
        [ShowIf("stackingSystem",Enums.StackingSystem.Dynamic)]
        [SerializeField] private GridData stackerGridData;

        [SerializeField] private AIMoneyStackerController moneyStackerController;

        #endregion

        #region Private Variables
        
        [ShowInInspector] private List<Vector3> gridPositionsData = new List<Vector3>();
        
        private Vector3 _gridPositions;

        private StackData _stackData;

        private GridData _gridData;

        #endregion

        #region Public Variables
        
        #endregion
        
        #endregion
        
        private void Awake()
        {
            GetData();
            SetGrid();
            SendGridDataToStacker();
        }

        private void GetData()
        {
            if (stackingSystem == StackingSystem.Dynamic)
            {
                stackerGridData = GetStackerGridData();
            }
            else
            {
                stackAreaGridData = GetAreaStackGridData();
            }
        }
        
        private GridData GetStackerGridData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem].DynamicGridDatas[(int)stackerType];
        }

        private GridData GetAreaStackGridData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem].StaticGridDatas[(int)stackAreaType];
        }

      
        public override void SetStackHolder(GameObject gameObject)
        {
            gameObject.transform.SetParent(transform);
        }
        
        
        public override void SetGrid()
        {
            if (stackingSystem == StackingSystem.Static)
            {
               _gridData = stackAreaGridData;
            }
            else
            {
                _gridData = stackerGridData;
            }
            var gridCount = _gridData.GridSize.x *_gridData.GridSize.y * _gridData.GridSize.z;
            for (int i = 0; i < gridCount; i++)
            {
                var modX = (int)(i % _gridData.GridSize.x);
                var divideZ =(int) (i / _gridData.GridSize.x); 
                var modZ = (int)(divideZ % _gridData.GridSize.z); 
                var divideXZ = (int)(i / (_gridData.GridSize.x * _gridData.GridSize.z));
                if (_gridData.isDynamic)
                {
                    _gridPositions = new Vector3(modX * _gridData.Offset.x,
                        modZ * _gridData.Offset.z,divideXZ * _gridData.Offset.y);
                }
                else
                {
                    _gridPositions = new Vector3(modX * _gridData.Offset.x,divideXZ * _gridData.Offset.y,
                        modZ * _gridData.Offset.z);
                    
                }
                gridPositionsData.Add(_gridPositions);
                
            }
        }
        
        public override void SendGridDataToStacker()
        {
            moneyStackerController.GetStackPositions(gridPositionsData);
        }
    }
}