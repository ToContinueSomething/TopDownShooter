using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Logic.UI.Button
{
    public class MenuButton : MonoBehaviour, IPointerClickHandler
    {
        private const string SceneName = "Menu";
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _gameStateMachine.Enter<LoadMenuState,string>(SceneName);
        }
    }
}