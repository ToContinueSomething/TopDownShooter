using System;
using System.Collections.Generic;
using Sources.Logic.Spawner;
using Sources.Logic.SpawnPoint;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Logic.Enemy
{
    public class EnemySimulation : Simulation
    {
        [SerializeField] private EnemyAudio _audio;
        
        private int _randomIndex;
        private EnemySpawner _spawner;
        private List<EnemySpawnPoint> _spawnPoints;
        private EnemyDeathCounter _enemyDeathCounter;

        public void Construct(EnemySpawner spawner, List<EnemySpawnPoint> spawnPoints, EnemyDeathCounter enemyDeathCounter)
        {
            _spawnPoints = spawnPoints;
            _spawner = spawner;
            _enemyDeathCounter = enemyDeathCounter;
        }

        protected override void SetEntity()
        {
            _randomIndex = Random.Range(0, _spawnPoints.Count);
            var enemy = _spawner.Spawn();

            if (enemy != null)
            {
                enemy.transform.position = _spawnPoints[_randomIndex].Position;
                enemy.gameObject.SetActive(true);
                enemy.ResetState();
                enemy.Died += OnEnemyDied;
            }
        }

        protected override void DisableActions()
        {
            _spawner.DisableAll();
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _enemyDeathCounter.Increase();
            _audio.Die();
        }
    }
}