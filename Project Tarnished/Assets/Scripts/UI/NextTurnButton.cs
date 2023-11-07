using ProjectLumina.Capabilities;
using ProjectTarnished.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class NextTurnButton : MonoBehaviour
    {
        [SerializeField]
        private GameStateMachineController _gameController;

        private Battleable _currentHero;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _gameController.onTurnAdvanced += UpdateCurrentHero;
        }

        private void UpdateCurrentHero(Battleable currentCharacter)
        {
            _currentHero = currentCharacter.GetComponent<Battleable>();
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        public void OnClick()
        {
            _currentHero.FinishTurn();
        }
    }
}