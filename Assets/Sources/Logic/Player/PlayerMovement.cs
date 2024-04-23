using UnityEngine;

namespace Sources.Logic.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody2D _rigidbody;
        private Vector2 _movementInput;
        private Vector2 _smoothedMovementInput;
        private Vector2 _movementInputSmoothVelocity;
        private bool _isActive = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0f;
        }
    
        public void Move(Vector2 input)
        {
            if(_isActive == false)
                return;
            
            _movementInput = input;
            SetPlayerVelocity();
            RotateInDirectionOfInput();
        }

        public void Disable()
        {
            _isActive = false;
            _rigidbody.velocity = Vector2.zero;
        }

        private void SetPlayerVelocity()
        {
            _smoothedMovementInput = Vector2.SmoothDamp(
                _smoothedMovementInput,
                _movementInput,
                ref _movementInputSmoothVelocity,
                0.1f);

            _rigidbody.velocity = _smoothedMovementInput * _speed;
        }
    
        private void RotateInDirectionOfInput()
        {
            if (_movementInput != Vector2.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
                Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                _rigidbody.MoveRotation(rotation);
            }
        }
    }
}
