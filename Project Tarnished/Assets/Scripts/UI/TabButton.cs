using UnityEngine;

namespace ProjectTarnished.UI
{
    public class TabButton : MonoBehaviour
    {
        [field: SerializeField]
        public UIPanel Panel { get; private set; }

        [SerializeField]
        private TabGroup _tabGroup;

        public void ClickButton()
        {
            _tabGroup.ToggleTab(Panel);
        }
    }
}