using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class AbilitySlot : MonoBehaviour
    {
        public Ability Ability { get; private set; }

        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private Image _icon;

        public void SetAbility(Ability ability)
        {
            Ability = ability;
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.enabled = true;
            _icon.sprite = sprite;
        }

        public void OnClick()
        {
            _controller.SetSelectedAbility(Ability);
        }
    }
}