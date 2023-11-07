using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _levelText, _XPText;

        [SerializeField]
        private HeroController _controller;

        private CharacterLevel _level;

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
            if (_controller.CurrentHero.TryGetComponent(out CharacterLevel level))
            {
                if (level != null)
                {
                    level.Level.onXPGain -= UpdateUI;
                }

                _level = level;
                _level.Level.onXPGain += UpdateUI; ;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _levelText.SetText($"Level {_level.Level.CurrentLevel}");
            _XPText.SetText($"{_level.Level.CurrentXP}/{_level.Level.RequiredXP}");
        }
    }
}