using System;
using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.Enemy;
using Sources.Logic.Player.Weapon;
using Sources.Logic.UI.Elements;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgressReader, IHealth, IAddeable
    {
        [SerializeField] private PlayerRollback _rollback;
        public int MaxValue { get; private set; }
        public int CurrentValue { get; private set; }

        public event Action ValueChanged;
        
        public void LoadProgress(PlayerProgress progress)
        {
            CurrentValue = progress.State.CurrentHP;
            MaxValue = progress.State.MaxHP;
        }

        public void TakeDamage(int damage)
        {
            if (_rollback.IsRollBack)
                return;

            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            CurrentValue -= damage;
            _rollback.Rollback();
            ValueChanged?.Invoke();
        }

        public void AddValue(int value)
        {
            if (CurrentValue > MaxValue)
                return;

            if (value < 0)
                throw new ArgumentOutOfRangeException();

            CurrentValue += value;
            ValueChanged?.Invoke();

            if (CurrentValue > MaxValue)
                CurrentValue = MaxValue;
        }

        public void Add(int value)
        {
            AddValue(value);
        }
    }
}