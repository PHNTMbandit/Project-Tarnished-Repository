using ProjectLumina.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class ActionPointsUI : MonoBehaviour
    {
        [SerializeField]
        private GameStateMachineController _gameController;

        [SerializeField]
        private TextMeshProUGUI _text;

        private CharacterAbilityPoints _characterAbilityPoints;

        private void Awake()
        {
            _gameController.onTurnAdvanced += UpdateCurrentHero;
        }

        private void UpdateCurrentHero(Battleable currentCharacter)
        {
            if (_characterAbilityPoints != null)
            {
                _characterAbilityPoints.onUseAbilityPoints -= UpdateUI;
            }

            _characterAbilityPoints = currentCharacter.GetComponent<CharacterAbilityPoints>();
            _characterAbilityPoints.onUseAbilityPoints += UpdateUI;

            UpdateUI();
        }

        public void UpdateUI()
        {
            _text.SetText($"{_characterAbilityPoints.CurrentAbilityPoints}/{_characterAbilityPoints.AbilityPoints.Value}");
        }
    }
}