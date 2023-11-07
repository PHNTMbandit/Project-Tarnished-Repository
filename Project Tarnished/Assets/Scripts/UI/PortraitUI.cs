using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class PortraitUI : MonoBehaviour
    {
        [SerializeField]
        private Image _portrait;

        [SerializeField]
        private HeroController _controller;

        private CharacterStatSheet _statSheet;

        private void Awake()
        {
            _controller.onHeroChange += Initialise;
        }

        private void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_controller.CurrentHero.TryGetComponent(out CharacterStatSheet statSheet))
            {
                _statSheet = statSheet;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _portrait.sprite = _statSheet.StatSheet.Portrait;
        }
    }
}