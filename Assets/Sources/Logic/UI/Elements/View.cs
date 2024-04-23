using TMPro;
using UnityEngine;

namespace Sources.Logic.UI.Elements
{
    public abstract class View<TInterface> : MonoBehaviour where TInterface : IValue
    {
        [SerializeField] protected TMP_Text Text;
        
        protected TInterface Interface;

        public void Construct(TInterface tInterface)
        {
            Interface = tInterface;
            Interface.ValueChanged += OnValueChanged;
            OnValueChanged();
        }

        private void OnDisable()
        {
            if(Interface != null)
                Interface.ValueChanged -= OnValueChanged;
        }

        protected virtual void OnValueChanged()
        {
            Text.text = $"{Interface.CurrentValue} / {Interface.MaxValue}";
        }
    }
}