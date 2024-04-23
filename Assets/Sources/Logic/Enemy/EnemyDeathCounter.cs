using Sources.Data;
using UnityEngine;

namespace Sources.Logic.Enemy
{
    public class EnemyDeathCounter : MonoBehaviour
    {
        private EnemySimulation _enemySimulation;
        private Score _score;
        
        public void Construct(Score score)
        {
            _score = score;
        }

        public void Increase()
        {
            _score.IncreaseValue();
        }
    }
}