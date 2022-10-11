using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private Transform manager;
        [SerializeField] private MeshRenderer weaponMeshRenderer;
        [SerializeField] private MeshRenderer sideMeshRenderer;

        #endregion

        #region Private Variables

        private WeaponData _data;
        
        #endregion

        #endregion
        
        
        public void SetWeaponData(WeaponData weaponData)
        {
            _data = weaponData;
        }
        
        public void ChangeAreaStatus(AreaType areaStatus)
        {
            if (areaStatus == AreaType.Base)
            {
                weaponMeshRenderer.enabled = false;
                sideMeshRenderer.enabled = false;
            }
            else
            {
                weaponMeshRenderer.enabled = true;
                sideMeshRenderer.enabled = _data.HasSideMesh;
            }
        }
    }
}