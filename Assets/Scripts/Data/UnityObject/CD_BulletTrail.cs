using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BulletTrail", menuName = "BaseDefense/CD_BulletTrail", order = 0)]
    public class CD_BulletTrail : ScriptableObject
    {
        public AnimationCurve WidthCurve;
        public float Time = 0.5f;
        public float MinVertexDistance = 0.1f;
        public Gradient ColorGradient;
        public Material Material;
        public int CornerVertices;
        public int EndCapVertices;

        
    }
}