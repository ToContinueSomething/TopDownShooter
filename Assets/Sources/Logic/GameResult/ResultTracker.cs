using System;
using Sources.Data;
using UnityEngine;

namespace Sources.Logic.GameResult
{
    public class ResultTracker : MonoBehaviour
    {
        private Score _score;
        private IHealth _playerHealth;

        private bool _isTracked;

        public event Action Win;
        public event Action Lose;

        public void Construct(Score score, IHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _score = score;

            _playerHealth.ValueChanged += OnHealthValueChanged;
            _score.ValueChanged += OnScoreValueChanged;
        }

        private void OnDisable()
        {
            _playerHealth.ValueChanged -= OnHealthValueChanged;
            _score.ValueChanged -= OnScoreValueChanged;
        }

        private void OnScoreValueChanged()
        {
            if (_score.CurrentValue >= _score.MaxValue) 
                Track(Win);
        }

        private void OnHealthValueChanged()
        {
            if (_playerHealth.CurrentValue <= 0) 
                Track(Lose);
        }

        private void Track(Action action)
        {
            action?.Invoke();
            _isTracked = true;
        }
    }
}