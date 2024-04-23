using UnityEngine;

namespace Sources.Logic.Player.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 5f;
        
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 direction)
        {
            _transform.up = direction;
            _rigidbody.velocity = direction * Speed;
        }
    }
}