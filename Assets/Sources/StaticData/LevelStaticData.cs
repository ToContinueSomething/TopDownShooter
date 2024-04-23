using System;
using System.Collections.Generic;
using Sources.Logic.SpawnPoint;
using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints;
        [SerializeField] private List<BonusSpawnPoint> _bonusSpawnPoints;
        
        public List<TSpawnPoint> GetSpawnPoints<TSpawnPoint>() where TSpawnPoint : SpawnPoint
        {
            if (typeof(TSpawnPoint) == typeof(EnemySpawnPoint))
                return _enemySpawnPoints as List<TSpawnPoint>;
            else if (typeof(TSpawnPoint) == typeof(BonusSpawnPoint))
                return _bonusSpawnPoints as List<TSpawnPoint>;
            else
                throw new InvalidOperationException();
        }
    }
}