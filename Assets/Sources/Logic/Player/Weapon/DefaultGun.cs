using System;
using UnityEngine;

namespace Sources.Logic.Player.Weapon
{
    public class DefaultGun : MonoBehaviour, IWeapon, IAddeable
    {
        [SerializeField] private PlayerAudio _audio;
        
        private readonly int _maxBullets = 20;
        
        private int _bullets = 10;
        private bool _isActive = true;
        
        public Vector3 Direction => transform.up;

        public int MaxValue => _maxBullets;
        public int CurrentValue => _bullets;
        public bool Recharged { get; private set; }
        
        public event Action ValueChanged;
        public event Action Shotted;

        public void Shoot()
        {
            if (CanShoot() == false)
            {
                throw new InvalidOperationException();
            }

            Recharged = false;
            _bullets--;
            _audio.PlayShoot();
            Shotted?.Invoke(); 
            ValueChanged?.Invoke();
        }
        
        
        public bool CanShoot()
        {
            if (_bullets > 0 && Recharged && _isActive)
                return true;

            _audio.PlayDontShoot();
            return false;
        }

        public void AddBullets(int bullets)
        {
            _bullets += bullets;

            if (_bullets > _maxBullets) 
                _bullets = _maxBullets;
            
            ValueChanged?.Invoke();
        }

        public void Add(int value)
        {
            AddBullets(value);
        }

        public void Recharge()
        {
            Recharged = true;
        }

        public void Disable()
        {
            _isActive = true;
        }
    }
}