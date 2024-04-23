using Sources.Logic.UI.Services.Factory;

namespace Sources.Infrastructure.States
{
    public class LoadMenuState : IPayloadedState<string>
    {
        private readonly IUIFactory _uiFactory;
        private SceneLoader _sceneLoader;

        public LoadMenuState(SceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter(string nameScene)
        {
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiFactory.CreateMenu();
        }

        public void Exit()
        {
        }
    }
}