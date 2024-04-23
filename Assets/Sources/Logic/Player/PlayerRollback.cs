using System;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerRollback : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _animator;
      
        private bool _isRollback;

        public bool IsRollBack => _isRollback;

        private void Awake()
        {
            SetIgnoreCollision(false);
        }

        public void Rollback()
        {
            SetIgnoreCollision(true);
            _isRollback = true;
            _animator.PlayHit();
        }

        private void OnFinished()
        {
            _isRollback = false;
            SetIgnoreCollision(false);
        }
        
        private void SetIgnoreCollision(bool isIgnore)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), isIgnore);
        }
    }
}