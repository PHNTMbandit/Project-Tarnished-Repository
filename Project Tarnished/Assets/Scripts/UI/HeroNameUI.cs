using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class HeroNameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

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
            _text.SetText($"{_statSheet.StatSheet.CharacterFirstName} {_statSheet.StatSheet.CharacterLastName}");
        }
    }
}