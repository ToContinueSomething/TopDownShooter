using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.States;
using Sources.Logic.UI.Button;
using Sources.Logic.UI.Services.Windows;
using Sources.Logic.UI.Windows;
using UnityEngine;

namespace Sources.Logic.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string MenuPath = "Menu";
        private const string WindowSettingsPath = "Windows/SettingsWindow";
        
        private readonly IAssetProvider _provider;
        private readonly IPersistentProgressService _progressService;

        private Transform _menuRoot;
        private IGameStateMachine _gameStateMachine;

        public UIFactory(IAssetProvider provider, IPersistentProgressService progressService, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _provider = provider;
            _progressService = progressService;
        }
        
        public void CreateMenu()
        {
           var menu =  _provider.Instantiate(MenuPath);
           _menuRoot = menu.transform;
           menu.GetComponentInChildren<PlayButton>().Construct(_gameStateMachine);
           menu.GetComponentInChildren<SettingsButton>();
        }

        public void CreateSettings()
        {
            _provider.Instantiate(WindowSettingsPath,_menuRoot).GetComponent<SettingsWindow>().Construct(_progressService);
        }
    }
}