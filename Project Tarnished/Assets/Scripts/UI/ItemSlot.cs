using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Items;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class ItemSlot : MonoBehaviour
    {
        public Item Item { get; private set; }

        [SerializeField]
        private Image _icon;

        public void SetItem(Item item)
        {
            Item = item;
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.enabled = true;
            _icon.sprite = sprite;
        }

        public void OnClick()
        {
        }
    }
}