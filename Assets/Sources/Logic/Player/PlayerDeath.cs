using Sources.Logic.Player.Weapon;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private CircleCollider2D _colider;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private DefaultGun _gun;
        
        private PlayerHealth _health;
        private bool _isDie;

        private void Awake()
        {
            _health = GetComponent<PlayerHealth>();
        }

        private void OnEnable()
        {
            _health.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged()
        {
            if(!_isDie && _health.CurrentValue <= 0)
                Die();
        }

        private void Die()
        {
            _isDie = true;
            _animator.PlayDie();
            _movement.Disable();
            _gun.Disable();
            _colider.enabled = false;
        }
    }
}