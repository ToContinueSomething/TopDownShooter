using Sources.Logic.Observers;
using Sources.Logic.Player;
using UnityEngine;

namespace Sources.Logic.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private CollisionObserver _collisionObserver;

        private void OnEnable()
        {
            _collisionObserver.Enter += OnEnter;
        }

        private void OnDisable()
        {
            _collisionObserver.Enter -= OnEnter;
        }

        private void OnEnter(Collision2D collision2D)
        {
            if (collision2D.transform.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}