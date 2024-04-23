using UnityEngine;

namespace Sources.Logic.Spawner
{
    public static class PoolObjectEnemyExtensions
    {
        public static void ConstructEnemy(this PoolObject<Enemy.Enemy> poolObject, Transform player)
        {
            foreach (var enemy in poolObject.Entities)
            {
                enemy.Construct(player);
            }
        }
    }
}