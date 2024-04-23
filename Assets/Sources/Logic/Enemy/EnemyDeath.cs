using Sources.Logic.Player.Weapon;
using UnityEngine;

namespace Sources.Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        
        private EnemyHealth _health;
        private AgentMoveToPlayer _agent;
        private EnemyAttack _attack;
        
        private void Awake()
        {
            _attack = GetComponent<EnemyAttack>();
            _agent = GetComponent<AgentMoveToPlayer>();
            _health = GetComponent<EnemyHealth>();
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
            if (_health.IsDeath)
            {
                _animator.PlayDie();
                _agent.enabled = false;
                _attack.enabled = false;
            }
        }
    }
}