using System;
using Sources.Logic.Player.Weapon;
using Sources.Logic.Spawner;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerShootingCompositeRoot : MonoBehaviour
    {
        private DefaultGun _defaultGun;
        private BulletSpawner _bulletSpawner;

        public void Construct(DefaultGun defaultGun, BulletSpawner bulletSpawner)
        {
            _defaultGun = defaultGun;
            _bulletSpawner = bulletSpawner;
            _defaultGun.Shotted += OnShooted;
        }

        private void OnDisable()
        {
            if (_defaultGun != null)
                _defaultGun.Shotted -= OnShooted;
        }

        private void OnShooted()
        {
            Bullet bullet = _bulletSpawner.Spawn();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = _defaultGun.transform.position;
            bullet.Move(_defaultGun.Direction);
        }
    }
}