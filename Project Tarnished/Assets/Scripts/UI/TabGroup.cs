using System.Collections.Generic;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> Buttons => _buttons;

        [SerializeField]
        private List<TabButton> _buttons = new();

        public void ToggleTab(UIPanel panel)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].Panel != panel)
                {
                    _buttons[i].Panel.Close();
                }
                else
                {
                    _buttons[i].Panel.Open();
                }
            }
        }
    }
}