using UnityEngine;
using UnityEngine.AI;

namespace Sources.Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Update()
        {
            if (_playerTransform != null)
            {
                SetDestination();
            }
        }

        private void SetDestination()
        {
            var position = _playerTransform.position;
            position.z = 0;
            _agent.destination = position;
        }
    }
}