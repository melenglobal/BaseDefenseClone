using System;

namespace Data.ValueObject
{   
    [Serializable]
    public struct EnemySpawnData
    {
        public int NumberOfEnemiesToSpawn;
        public float SpawnDelay;
        public int RandomMaxRange;
        public int MinPossibilityToSpawnEnemy;
    }
}