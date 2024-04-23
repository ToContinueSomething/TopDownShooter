using Sources.Logic.Observers;
using UnityEngine;

namespace Sources.Logic.Player.Weapon
{
    public class BulletAttack : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _observer;
        [SerializeField] private int _damage;
        
        public int Damage => _damage;
        
        private void OnEnable()
        {
            _observer.Enter += OnEnter;
        }

        private void OnDisable()
        {
            _observer.Enter -= OnEnter;
        }

        private void OnEnter(Collider2D collider)
        {
            if (collider.transform.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_damage);
            }

            gameObject.SetActive(false);
        }
    }
}