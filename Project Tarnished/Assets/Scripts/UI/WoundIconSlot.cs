using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class WoundIconSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _border, _icon;

        public void SetIcon(Sprite sprite, Sprite border)
        {
            _icon.enabled = true;
            _icon.sprite = sprite;
            _border.sprite = border;
        }

        public void ResetSlot(Sprite border)
        {
            _icon.enabled = false;
            _border.sprite = border;
        }
    }
}