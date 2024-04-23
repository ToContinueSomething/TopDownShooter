using UnityEngine;
using UnityEngine.AI;

namespace Sources.Logic.Enemy
{
    public class RotateToPlayer : MonoBehaviour
    {
        private Transform _player;
        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _previousPosition;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _transform = transform;
            _previousPosition = _transform.position;
        }

        public void Construct(Transform player)
        {
            _player = player;
        }

        private void Update()
        {
            if (_player)
            {
                SetDirection();
            }
        }

        private void SetDirection()
        {
            var direction = (Vector2) _agent.nextPosition - (Vector2) _previousPosition;
            _transform.up = direction;
            _previousPosition = _transform.position;
        }
    }
}