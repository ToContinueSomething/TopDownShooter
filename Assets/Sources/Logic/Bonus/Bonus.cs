using System;
using Sources.Logic.Observers;
using Sources.Logic.Player;
using Sources.Logic.Player.Weapon;
using UnityEngine;

namespace Sources.Logic.Bonus
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField] private BonusType _type;
        [SerializeField] private int _value;
        [SerializeField] private TriggerObserver _triggerObserver;
        
        public event Action Collected;

        private void OnEnable()
        {
            _triggerObserver.Enter += OnEnter;
        }

        private void OnDisable()
        {
            _triggerObserver.Enter -= OnEnter;
        }

        private void OnEnter(Collider2D collider)
        {
            switch (_type)
            {
                case BonusType.Health:
                    TryAcceptBonus<PlayerHealth>(collider);
                    break;
                case BonusType.Cartridges:
                    TryAcceptBonus<DefaultGun>(collider);
                    break;
            }
        }

        private bool TryAcceptBonus<TComponent>(Collider2D collider) where TComponent : IAddeable
        {
            var component = collider.GetComponentInChildren<TComponent>();

            if (component != null)
            {
                component.Add(_value);
                Collected?.Invoke();
                gameObject.SetActive(false);
                return true;
            }

            return false;
        }
    }
}