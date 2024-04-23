using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;

namespace Sources.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string SceneName = "Menu";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        
        public LoadProgressState(GameStateMachine gameStateMachine,IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadMenuState, string>(SceneName);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = GetNewProgress();
        }

        private PlayerProgress GetNewProgress()
        {
            var playerProgress = new PlayerProgress();
            
            playerProgress.State.MaxHP = 100;
            playerProgress.State.ResetHp();
            
            playerProgress.WorldData.Score.MaxValue = 20;
            playerProgress.WorldData.Score.Restart();
            
            return playerProgress;
        }

        public void Exit()
        {
            
        }
    }
}