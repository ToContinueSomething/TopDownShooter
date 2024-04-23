using UnityEngine;

namespace Sources.Logic.Spawner
{
    public class EnemySpawner : Spawner<Enemy.Enemy>
    {
        public EnemySpawner Construct(Transform playerTransform)
        {
            this.ConstructEnemy(playerTransform);
            return this;
        }
        
    }
}