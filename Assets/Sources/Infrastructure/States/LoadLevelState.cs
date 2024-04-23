using System.Collections.Generic;
using Sources.CameraLogic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.Inform;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.Logic.Bonus;
using Sources.Logic.Enemy;
using Sources.Logic.Spawner;
using Sources.Logic.SpawnPoint;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IInformProgressReaderService _informProgressReaderService;

        public LoadLevelState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService,
            IInformProgressReaderService informProgressReaderService)
        {
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _informProgressReaderService = informProgressReaderService;
        }

        public void Enter(string nameScene)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _progressService.Progress.WorldData.Score.Restart();
            _progressService.Progress.State.ResetHp();
            
            InitGameWorld();
            _informProgressReaderService.Inform();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateHud();
            _gameFactory.CreateBulletSpawner();

            var hero = _gameFactory.CreatePlayer();
            var levelData = _gameFactory.StaticDataService.GetLevelData();

            _gameFactory.CreateUI();
            _gameFactory.CreateEnemyDeathCounter();
            
            InitEnemies(levelData);
            InitBonuses(levelData);
            CameraFollow(hero);
            
            _gameFactory.CreateGameResult();
        }

        private void InitBonuses(LevelStaticData levelStaticData)
        {
            var spawnPoints = InitSpawnPoints<BonusSpawnPoint>(levelStaticData);

            BonusSpawner bonusSpawner = _gameFactory.CreateBonusSpawner();
            BonusSimulation simulation = _gameFactory.CreateBonusSimulation(bonusSpawner, spawnPoints);

            simulation.Simulate();
        }

        private void InitEnemies(LevelStaticData levelStaticData)
        {
            var spawnPoints = InitSpawnPoints<EnemySpawnPoint>(levelStaticData);
            EnemySpawner enemySpawner = _gameFactory.CreateEnemySpawner(spawnPoints);
            EnemySimulation simulation = _gameFactory.CreateEnemySimulation(enemySpawner, spawnPoints);

            simulation.Simulate();
        }

        private List<TSpawnPoint> InitSpawnPoints<TSpawnPoint>(LevelStaticData levelData) where TSpawnPoint : SpawnPoint
        {
            var spawnPoints = new List<TSpawnPoint>();
            var levelSpawnPoints = levelData.GetSpawnPoints<TSpawnPoint>();

            foreach (var spawnPoint in levelSpawnPoints)
                spawnPoints.Add(_gameFactory.CreateSpawnPoint<TSpawnPoint>(spawnPoint.Position));

            return spawnPoints;
        }
        

        public void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}