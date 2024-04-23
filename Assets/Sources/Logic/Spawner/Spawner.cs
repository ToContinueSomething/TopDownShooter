using System.Collections.Generic;
using UnityEngine;

namespace Sources.Logic.Spawner
{
    public abstract class Spawner<T> : PoolObject<T> where T : MonoBehaviour
    {
        [SerializeField] private List<T> _prefab;
        
        private void Awake()
        {
            Initialize(_prefab);
        }

        public T Spawn()
        {
            return GetEntity();
        }

        protected virtual T GetEntity()
        {
            return GetEntityOrNull();
        }

        public void DisableAll()
        {
            DisableAllEntities();
        }
    }
}