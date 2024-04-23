using System;
using UnityEngine;

namespace Sources.Logic.Observers
{
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private LayerMask _ignoreMask;

        public event Action<Collider2D> Enter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_ignoreMask.Compare(other.gameObject.layer))
                return;
            
            Enter?.Invoke(other);
        }
    }
}