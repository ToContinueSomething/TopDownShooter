using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Logic.Enemy;
using Sources.Logic.Spawner;
using Sources.Logic.SpawnPoint;
using UnityEngine;

namespace Sources.Logic.Bonus
{
    public class BonusSimulation : Simulation
    {
        private const float DelayBetweenSpawn = 5f;
        private List<BonusSpawnPoint> _spawnPoints;
        private BonusSpawner _spawner;
        private BonusSpawnPoint _spawnPoint;

        public void Construct(BonusSpawner spawner, List<BonusSpawnPoint> spawnPoints)
        {
            _spawnPoints = spawnPoints;
            _spawner = spawner;
        }

        protected override void SetEntity()
        {
            if (_spawnPoint == null)
                throw new InvalidOperationException();

            var bonus = _spawner.Spawn();

            if (bonus != null)
            {
                _spawnPoint.SetBonus(bonus);
                bonus.transform.position = _spawnPoint.Position;
                bonus.gameObject.SetActive(true);
            }
        }

        protected override void DisableActions()
        {
            _spawner.DisableAll();
        }

        protected override object GetInstruction()
        {
            _spawnPoint = _spawnPoints.FirstOrDefault(p => p.IsBusy == false);

            if (_spawnPoint == null)
                return null;
                
            SetEntity();
            return new WaitForSeconds(DelayBetweenSpawn);
        }
    }
}