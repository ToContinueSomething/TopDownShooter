using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(int value)
        {
            _image.fillAmount = value / 100f;
        }
    }
}