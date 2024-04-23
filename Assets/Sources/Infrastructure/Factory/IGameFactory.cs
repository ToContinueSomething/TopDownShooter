using System.Collections.Generic;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.StaticData;
using Sources.Logic;
using Sources.Logic.Bonus;
using Sources.Logic.Enemy;
using Sources.Logic.Spawner;
using Sources.Logic.SpawnPoint;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> SavedProgressReaders { get; }
        IStaticDataService StaticDataService { get; }
        void Cleanup();
        void CreateHud();
        void CreateUI();
        GameObject CreatePlayer();
        
        void CreateBulletSpawner();
        TSpawnPoint CreateSpawnPoint<TSpawnPoint>(Vector3 at) where TSpawnPoint : SpawnPoint;
        EnemySpawner CreateEnemySpawner(List<EnemySpawnPoint> spawnPoints);
        EnemySimulation CreateEnemySimulation(EnemySpawner enemySpawner, List<EnemySpawnPoint> spawnPoints);
        void CreateEnemyDeathCounter();
        BonusSpawner CreateBonusSpawner();
        BonusSimulation CreateBonusSimulation(BonusSpawner bonusSpawner, List<BonusSpawnPoint> spawnPoints);
        
        void CreateWinText();
        void CreateLoseText();
        void CreateGameResult();
    }
}