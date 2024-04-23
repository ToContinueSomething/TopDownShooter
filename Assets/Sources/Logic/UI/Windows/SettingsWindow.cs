using Sources.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Windows
{
    public class SettingsWindow : MonoBehaviour
    {
        private const int MediumSettingIndex = 3;
        private const int UltraSettingIndex = 6;
        private const int DontVSynkIndex = 0;

        [SerializeField] private UnityEngine.UI.Button _closeButton;
        [SerializeField] private Toggle _joystickToggle;
        [SerializeField] private Toggle _presetsToggle;

        private IPersistentProgressService _persistentProgressService;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
            
            _joystickToggle.onValueChanged.AddListener(OnToggleJoystickChanged);
            _presetsToggle.onValueChanged.AddListener(OnTogglePresetsChanged);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(() => Destroy(gameObject));
            
            _joystickToggle.onValueChanged.RemoveListener(OnToggleJoystickChanged);
            _presetsToggle.onValueChanged.RemoveListener(OnTogglePresetsChanged);
        }

        private void OnTogglePresetsChanged(bool isOn)
        {
            QualitySettings.SetQualityLevel(isOn ? UltraSettingIndex : MediumSettingIndex);
            QualitySettings.vSyncCount = isOn ? DontVSynkIndex : 2;
        }

        private void OnToggleJoystickChanged(bool isOn)
        {
            _persistentProgressService.Progress.InputState.IsDynamicJoystick = isOn;
        }
    }
}