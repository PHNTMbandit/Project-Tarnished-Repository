using ProjectTarnished.Capabilities;
using ProjectTarnished.Controllers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class HeroSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Commandable Hero { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _heroName;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private HeroController _heroController;

        [PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        private Sprite _disabled, _unselectedIdle, _unselectedHover, _selectedIdle;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Hero != null && _heroController.CurrentHero != Hero)
            {
                SetImage(_unselectedHover);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Hero != null && _heroController.CurrentHero != Hero)
            {
                SetImage(_unselectedIdle);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Hero != null)
            {
                _heroController.ChangeHero(Hero);
            }
        }

        public void SetHero(Commandable hero)
        {
            Hero = hero;

            if (Hero == _heroController.CurrentHero)
            {
                SetImage(_selectedIdle);
            }
            else
            {
                SetImage(_unselectedIdle);
            }
        }

        public void SetName(string name)
        {
            _heroName.SetText(name);
        }

        public void SetImage(Sprite image)
        {
            _image.sprite = image;
        }

        public void ResetButton()
        {
            SetHero(null);
            SetName("Empty Hero Slot");
            SetImage(_disabled);
        }
    }
}