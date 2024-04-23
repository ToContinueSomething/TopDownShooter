using Sources.Infrastructure.Services.Input;
using Sources.Logic.Player.Weapon;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerInputRouter : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerMovement _movement;
        private DefaultGun _gun;

        public void Construct(IInputService inputService, DefaultGun gun, PlayerMovement movement)
        {
            _inputService = inputService;
            _gun = gun;
            _movement = movement;
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp())
            {
                if(_gun.CanShoot())
                    _gun.Shoot();
            }
        }

        private void FixedUpdate()
        {
            _movement.Move(_inputService.Axis);
        }
    }
}