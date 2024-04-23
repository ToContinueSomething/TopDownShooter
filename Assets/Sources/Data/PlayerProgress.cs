using System;
using UnityEngine;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State State;
        public InputState InputState;
        
        public PlayerProgress()
        {
            WorldData = new WorldData();
            State = new State();
            InputState = new InputState();
        }
    }

    [Serializable]
    public class InputState
    {
        public bool IsDynamicJoystick;
    }
}