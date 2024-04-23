using System;
using System.Collections.Generic;
using SimpleInputNamespace;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.Input;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.StaticData;
using Sources.Infrastructure.States;
using Sources.Logic;
using Sources.Logic.Bonus;
using Sources.Logic.Enemy;
using Sources.Logic.GameResult;
using Sources.Logic.Player;
using Sources.Logic.Player.Weapon;
using Sources.Logic.Spawner;
using Sources.Logic.SpawnPoint;
using Sources.Logic.UI.Button;
using Sources.Logic.UI.Elements;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _progressService;

        private IHealth _playerHealth;
        private GameObject _player;
        private BulletSpawner _bulletSpawner;
        private PlayerShootingCompositeRoot _playerShootingCompositeRoot;
        private IWeapon _weapon;
        private EnemyDeathCounter _enemyDeathCounter;
        private IGameStateMachine _gameStateMachine;
        private EnemySimulation _enemySimulation;
        private BonusSimulation _bonusSimulation;
        private GameObject _uiRoot;

        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();
        public IStaticDataService StaticDataService => _staticDataService;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService,
            IInputService inputService, IPersistentProgressService progressService,IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _inputService = inputService;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void Cleanup()
        {
            SavedProgressReaders.Clear();
        }

        public void CreateHud()
        {
          var canvasHud = InstantiateRegistered(AssetPath.HudPath);
          canvasHud.GetComponentInChildren<Joystick>().SetStateDynamic(_progressService.Progress.InputState.IsDynamicJoystick);
        }

        public void CreateUI()
        {
            _uiRoot = InstantiateRegistered(AssetPath.UIPath);
            _uiRoot.GetComponentInChildren<ActorUI>().Construct(_playerHealth);
            _uiRoot.GetComponentInChildren<ScoreCounter>().Construct(_progressService.Progress.WorldData.Score);
            _uiRoot.GetComponentInChildren<MagazineWeaponView>().Construct(_weapon);
            _uiRoot.GetComponentInChildren<MenuButton>().Construct(_gameStateMachine);
        }

        public void CreateEnemyDeathCounter()
        {
            _enemyDeathCounter = InstantiateRegistered(AssetPath.EnemyDeathCounterPath).GetComponent<EnemyDeathCounter>();
            _enemyDeathCounter.Construct(_progressService.Progress.WorldData.Score);
        }

        public void CreateGameResult()
        {
            var gameResult = InstantiateRegistered(AssetPath.GameResultPath);
            gameResult.GetComponent<ResultTracker>().Construct(_progressService.Progress.WorldData.Score,_playerHealth);
            gameResult.GetComponent<ResultHandler>().Construct(new List<Simulation>(){_enemySimulation,_bonusSimulation},this,_gameStateMachine);
        }

        public GameObject CreatePlayer()
        {
            _player = InstantiateRegistered(AssetPath.PlayerPath);

            var gun = _player.gameObject.GetComponentInChildren<DefaultGun>();
            var movement = _player.gameObject.GetComponent<PlayerMovement>();

            _playerHealth = _player.GetComponent<IHealth>();
            _player.GetComponentInChildren<PlayerShootingCompositeRoot>().Construct(gun, _bulletSpawner);
            _weapon = _player.GetComponentInChildren<IWeapon>();

            _player.GetComponentInChildren<PlayerInputRouter>().Construct(_inputService, gun, movement);

            return _player;
        }

        public void CreateBulletSpawner()
        {
            _bulletSpawner = InstantiateRegistered(AssetPath.BulletSpawnerPath).GetComponent<BulletSpawner>();
        }

        public TSpawnPoint CreateSpawnPoint<TSpawnPoint>(Vector3 at) where TSpawnPoint : SpawnPoint
        {
            return InstantiateRegistered(GetSpawnPointPath<TSpawnPoint>(), at).GetComponent<TSpawnPoint>();
        }

        private string GetSpawnPointPath<TSpawnPoint>()
        {
            if (typeof(TSpawnPoint) == typeof(EnemySpawnPoint))
                return AssetPath.EnemySpawnPointPath;
            if (typeof(TSpawnPoint) == typeof(BonusSpawnPoint))
                return AssetPath.BonusSpawnPointPath;
            else
                throw new InvalidOperationException();
        }

        public EnemySpawner CreateEnemySpawner(List<EnemySpawnPoint> spawnPoints)
        {
            return InstantiateRegistered(AssetPath.SpawnerPath).GetComponent<EnemySpawner>().Construct(_player.transform);
        }

        public EnemySimulation CreateEnemySimulation(EnemySpawner enemySpawner, List<EnemySpawnPoint> spawnPoints)
        {
            _enemySimulation = InstantiateRegistered(AssetPath.EnemySimulationPath).GetComponent<EnemySimulation>();
            _enemySimulation.Construct(enemySpawner, spawnPoints, _enemyDeathCounter);
            return _enemySimulation;
        }

        public BonusSimulation CreateBonusSimulation(BonusSpawner bonusSpawner, List<BonusSpawnPoint> spawnPoints)
        {
             _bonusSimulation = InstantiateRegistered(AssetPath.BonusSimulationPath).GetComponent<BonusSimulation>();
             _bonusSimulation.Construct(bonusSpawner, spawnPoints);
            return _bonusSimulation;
        }
        
        public void CreateWinText()
        {
            InstantiateRegistered(AssetPath.WinTextPath, _uiRoot.transform);
        }

        public void CreateLoseText()
        {
            InstantiateRegistered(AssetPath.LoseTextPath, _uiRoot.transform);
        }

        public BonusSpawner CreateBonusSpawner()
        {
            return InstantiateRegistered(AssetPath.BonusSpawnerPath).GetComponent<BonusSpawner>();
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(path, at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        } 
        
        private GameObject InstantiateRegistered(string path, Transform parent)
        {
            GameObject gameObject = _assetProvider.Instantiate(path, parent);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            SavedProgressReaders.Add(progressReader);
        }
    }
}