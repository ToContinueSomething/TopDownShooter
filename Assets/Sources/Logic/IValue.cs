using System;

namespace Sources.Logic
{
    public interface IValue
    {
        public int MaxValue { get; }
        public int CurrentValue { get; }
        public event Action ValueChanged;
    }
}