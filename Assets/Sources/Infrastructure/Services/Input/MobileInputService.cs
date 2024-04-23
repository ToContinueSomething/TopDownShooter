using System;
using UnityEngine;

namespace Sources.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Button = "Fire";

        public Vector2 Axis => SimpleInputAxis();

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Button);
        }
        
        private Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}