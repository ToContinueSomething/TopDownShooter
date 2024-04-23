using UnityEngine;

namespace Sources.Logic.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Enemy _enemy;
        private bool _isDeath;
        
        private readonly int _isDeathHash = Animator.StringToHash("IsDeath");

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _animator = GetComponent<Animator>();
        }

        public void PlayDie()
        {
            _animator.SetBool(_isDeathHash,true);

        }

        private void OnDied()
        {
            _enemy.Die();
        }

        public void ResetState()
        {
            _animator.SetBool(_isDeathHash,false);
        }
    }
}