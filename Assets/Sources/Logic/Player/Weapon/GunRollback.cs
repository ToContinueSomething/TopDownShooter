using System;
using UnityEngine;

namespace Sources.Logic.Player.Weapon
{
    public class GunRollback : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private DefaultGun _gun;
        
        private float _acumulatedTime;

        private void Update()
        {
            if(_gun.Recharged)
                return;

            _acumulatedTime += Time.deltaTime;

            if (_acumulatedTime >= _cooldown)
            {
                _gun.Recharge();
                _acumulatedTime = 0;
            }
        }
    }
}