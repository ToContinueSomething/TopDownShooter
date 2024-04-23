using System;
using UnityEngine;

namespace Sources.Logic.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            if (_health != null)
                _health.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged()
        {
            _hpBar.SetValue(_health.CurrentValue);
        }
    }
}