using System;
using System.Collections;
using System.Collections.Generic;
using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Logic.Timer
{
    public class Timer
    {
        private float _accumulatedTime;
        private readonly float _time;
        private ICoroutineRunner _coroutineRunner;
        private bool IsEnd => _time <= _accumulatedTime;
        
        private Action OnEnd;
        
        public Timer(ICoroutineRunner coroutineRunner, float time, Action onEnd)
        {
            _coroutineRunner = coroutineRunner;
            _time = time;
            OnEnd = onEnd;
        }

        public void Start()
        {
            _coroutineRunner.StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (IsEnd == false)
            {
                _accumulatedTime += Time.deltaTime;
                yield return null;
            }
            
            OnEnd.Invoke();
        }
    }
}
