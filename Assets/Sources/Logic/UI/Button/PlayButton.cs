using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Logic.UI.Button
{
    public class PlayButton : MonoBehaviour, IPointerClickHandler
    {
        private const string NameScene = "Game";
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _gameStateMachine.Enter<LoadLevelState,string>(NameScene);
        }
    }
}