using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Enemy", menuName = "BaseDefense/CD_Enemy", order = 0)]
    public class CD_Enemy : ScriptableObject
    {
        public AmmoWorkerAIData AmmoWorkerAIData;
        public MoneyWorkerAIData MoneyWorkerAIData;
        public MineWorkerAIData MineWorkerAIData;
        public SoldierAIData SoldierAIData;
        public EnemyAIData EnemyAIData; // Make list when level
    }
}