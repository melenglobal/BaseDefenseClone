using System;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public MineWorkerAIData MineWorkerAIData;


        private void Awake()
        {
           MineWorkerAIData= GetData();
        }

        private void Start()
        {
            Debug.Log(MineWorkerAIData.Capacity + " / " +MineWorkerAIData.Speed);
        }

        private MineWorkerAIData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").MineWorkerAIData;
    }
}