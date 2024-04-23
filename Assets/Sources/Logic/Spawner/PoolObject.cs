using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Logic.Spawner
{
    public class PoolObject<TEntity> : MonoBehaviour where TEntity : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private Transform _container;
        
        private List<TEntity> _entities;
        private int _randomIndex;

        public IReadOnlyCollection<TEntity> Entities => _entities; 

        protected void Initialize(List<TEntity> entityPrefabs)
        {
            _entities = new List<TEntity>();

            for (int i = 0; i < entityPrefabs.Count; i++)
            {
                for (int j = 0; j < _count; j++)
                {
                    var spawnedEntity = Instantiate(entityPrefabs[i], _container);
                    spawnedEntity.gameObject.SetActive(false);
                    _entities.Add(spawnedEntity);
                }
            }
        }

        protected TEntity GetEntityOrNull()
        {
            return _entities.FirstOrDefault(e => e.gameObject.activeSelf == false);
        }

        protected TEntity GetRandomEntityOrNull()
        {
           var checkEntity = _entities.FirstOrDefault(e => e.gameObject.activeSelf == false);

           if (checkEntity == null)
               return null;

           _randomIndex = Random.Range(0, _entities.Count);

           while (_entities[_randomIndex].gameObject.activeSelf)
           {
               _randomIndex = Random.Range(0, _entities.Count);
           }

           return _entities[_randomIndex];
        }

        protected void DisableAllEntities()
        {
            foreach (var entity in _entities)
            {
                entity.gameObject.SetActive(false);
            }
        }
    }
}