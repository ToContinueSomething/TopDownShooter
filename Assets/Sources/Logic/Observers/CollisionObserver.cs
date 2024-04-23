using System;
using UnityEngine;

namespace Sources.Logic.Observers
{
    internal class CollisionObserver : MonoBehaviour
    {
        [SerializeField] private LayerMask _ignoreMask;

        public event Action<Collision2D> Enter;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_ignoreMask.Compare(other.gameObject.layer))
                return;

            Enter?.Invoke(other);
        }
    }
}