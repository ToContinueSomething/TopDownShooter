using UnityEngine;

namespace Sources.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _maxHorizontalPosition;
        [SerializeField] private Transform _minHorizontalPosition;
        
        [SerializeField] private Transform _minVerticalPosition;
        [SerializeField] private Transform _maxVerticalPosition;

        private Transform _following;
        private Transform _transform;
        private Vector2 _clamped;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            if (_following == null)
                return;
            
            ClampPosition();
        }

        private void ClampPosition()
        {
            _clamped.x = Mathf.Clamp(_following.position.x, _minHorizontalPosition.position.x, _maxHorizontalPosition.position.x);
            _clamped.y = Mathf.Clamp(_following.position.y, _minVerticalPosition.position.y, _maxVerticalPosition.position.y);
            
            _transform.position =  new Vector3(_clamped.x,_clamped.y,_transform.position.z);
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }
    }
}