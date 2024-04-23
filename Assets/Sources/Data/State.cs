using System;
using UnityEngine;

namespace Sources.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHP;
        public int MaxHP;

        public void ResetHp()
        {
            CurrentHP = MaxHP;
        }
    }
}