using UnityEngine;

namespace Sources.Logic.Player
{
   [RequireComponent(typeof(Animator))]
   public class PlayerAnimator : MonoBehaviour
   {
      [SerializeField] private Rigidbody2D _rigidbody;

      private Animator _animator;
      private bool _isDeath;
      
      private readonly int _isDeathHash = Animator.StringToHash("IsDeath");
      private readonly int _walkingHash = Animator.StringToHash("Walking");
      private static readonly int _hitHash = Animator.StringToHash("Hit");

      private void Awake()
      {
         _animator = GetComponent<Animator>();
      }

      private void Update()
      {
         _animator.SetFloat(_walkingHash, _rigidbody.velocity.magnitude);
      }

      public void PlayDie()
      {
         _isDeath = true;
         _animator.SetBool(_isDeathHash,_isDeath);
      }

      public void PlayHit()
      {
         _animator.SetTrigger(_hitHash);
      }
   }
}
