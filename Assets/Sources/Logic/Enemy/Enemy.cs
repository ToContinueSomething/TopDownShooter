using System;
using UnityEngine;

namespace Sources.Logic.Enemy
{
    [RequireComponent(typeof(AgentMoveToPlayer))]
    public class Enemy : MonoBehaviour
    {
        private AgentMoveToPlayer _agentMoveToPlayer;
        private RotateToPlayer _rotateToPlayer;
        private EnemyHealth _health;
        private EnemyAnimator _animator;
        private EnemyAttack _attack;

        public event Action<Enemy> Died;

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
            _agentMoveToPlayer = GetComponent<AgentMoveToPlayer>();
            _animator = GetComponent<EnemyAnimator>();
            _rotateToPlayer = GetComponent<RotateToPlayer>();
            _attack = GetComponent<EnemyAttack>();
        }

        public void Construct(Transform player)
        {
            _agentMoveToPlayer.Construct(player);
            _rotateToPlayer.Construct(player);
        }

        public void Die()
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }

        public void ResetState()
        {
            _agentMoveToPlayer.enabled = true;
            _attack.enabled = true;
            
            _animator.ResetState();
            _health.ResetState();
        }
    }
}