using System.Collections;
using UnityEngine;

namespace Sources.Logic
{
    public abstract class Simulation : MonoBehaviour
    {
        private const float DelayForSpawn = 3f;
        
        private bool _isActive;
        
        public void Simulate()
        {
            _isActive = true;

           StartCoroutine(UpdateSimulate());
        }

        protected abstract void SetEntity();
        protected abstract void DisableActions();

        private IEnumerator UpdateSimulate()
        {
            while (_isActive)
            {
                yield return GetInstruction();
            }
        }

        protected virtual object GetInstruction()
        {
            SetEntity();
            return new WaitForSeconds(DelayForSpawn);
        }
        
        public void Stop()
        {
            _isActive = false;
            DisableActions();
        }
    }
}