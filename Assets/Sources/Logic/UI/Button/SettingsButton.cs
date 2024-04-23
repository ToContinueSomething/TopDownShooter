using System;
using Sources.Infrastructure.Services;
using Sources.Logic.UI.Services.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Logic.UI.Button
{
    public class SettingsButton : MonoBehaviour, IPointerClickHandler
    {
        private IWindowService _windowService;
        
        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _windowService.Open(WindowId.Settings);
        }
    }
}