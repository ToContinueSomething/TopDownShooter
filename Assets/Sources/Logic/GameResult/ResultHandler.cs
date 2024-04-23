using System.Collections.Generic;
using Sources.Infrastructure;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.States;
using UnityEngine;

namespace Sources.Logic.GameResult
{
    public class ResultHandler : MonoBehaviour,ICoroutineRunner
    {
        [SerializeField] private ResultTracker _tracker;
        
        private const float Delay = 3.5f;
        private const string SceneName = "Menu";

        private IGameFactory _gameFactory;
        private Timer.Timer _timer;
        private List<Simulation> _simulations;
        private IGameStateMachine _gameStateMachine;

        public void Construct(List<Simulation> simulations, IGameFactory gameFactory,
            IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _simulations = simulations;
            _gameFactory = gameFactory;
            _timer = new Timer.Timer(this, Delay, OnFinished);
        }

        private void OnEnable()
        {
            _tracker.Win += OnWin;
            _tracker.Lose += OnLose;
        }

        private void OnDisable()
        {
            _tracker.Win -= OnWin;
            _tracker.Lose -= OnLose;
        }

        private void OnWin()
        {
            StopSimulations();
            _gameFactory.CreateWinText();
            _timer.Start();
        }

        private void OnLose()
        {
            StopSimulations();
            _gameFactory.CreateLoseText();
            _timer.Start();
        }
        

        private void StopSimulations()
        {
            foreach (var simulation in _simulations)
            {
                simulation.Stop();
            }
        }

        private void OnFinished()
        {
            _gameStateMachine.Enter<LoadMenuState,string>(SceneName);  
        }
    }
}