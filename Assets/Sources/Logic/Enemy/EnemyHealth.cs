using System;
using UnityEngine;

namespace Sources.Logic.Enemy
{
    internal class EnemyHealth : MonoBehaviour,IHealth
    {
        private int _currentHealth = 10;
        public bool IsDeath => _currentHealth <= 0;
        public int MaxValue { get; private set; }
        public int CurrentValue => _currentHealth;

        public event Action ValueChanged;

        private void Awake()
        {
            MaxValue = _currentHealth;
           
        }

        public void TakeDamage(int damage)
        {
            if(IsDeath)
                return;
            
            if (damage < 0)
                throw new ArgumentOutOfRangeException();
            
            _currentHealth -= damage;
            ValueChanged?.Invoke();
        }

        public void ResetState()
        {
            _currentHealth = MaxValue;
        }
    }
}