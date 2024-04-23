using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Logic.UI.Button
{
    public class ExitButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.Quit();
        }
    }
}