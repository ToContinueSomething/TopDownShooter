using System;
using Sources.Logic;
using Sources.Logic.UI.Elements;

namespace Sources.Data
{
    [Serializable]
    public class Score : IValue
    {
        public int CurrentValue { get; private set; }
        public int MaxValue { get; set; }

        public event Action ValueChanged;
        
        public void IncreaseValue()
        {
            if(CurrentValue >= MaxValue)
                return;
            
            CurrentValue++;

            ValueChanged?.Invoke();
        }

        public void Restart()
        {
            CurrentValue = 0;
        }
    }
}