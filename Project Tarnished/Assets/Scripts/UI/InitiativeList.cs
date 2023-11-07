using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectTarnished.Controllers;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class InitiativeList : MonoBehaviour
    {
        [SerializeField]
        private GameStateMachineController _controller;

        [SerializeField]
        private InitiativeButton _templateNPCButton, _templatePlayerButton;

        [SerializeField]
        private Transform _transform;

        private readonly List<InitiativeButton> _buttons = new();

        private void Awake()
        {
            _templateNPCButton.gameObject.SetActive(false);
            _templatePlayerButton.gameObject.SetActive(false);

            _controller.onInitiativeChanged += GenerateList;
            _controller.onTurnAdvanced += SetCurrentPortrait;
        }

        public void GenerateList()
        {
            ResetList();

            foreach (Battleable battleable in _controller.InitiativeOrder)
            {
                if (battleable.StatSheet.CharacterType == Data.CharacterType.Hero)
                {
                    InitiativeButton button = Instantiate(_templatePlayerButton.gameObject, _transform).GetComponent<InitiativeButton>();
                    button.gameObject.SetActive(true);
                    button.SetCharacter(battleable);
                    button.SetPortrait(battleable.StatSheet.Portrait);

                    _buttons.Add(button);
                }
                else
                {
                    InitiativeButton button = Instantiate(_templateNPCButton.gameObject, _transform).GetComponent<InitiativeButton>();
                    button.gameObject.SetActive(true);
                    button.SetCharacter(battleable);
                    button.SetPortrait(battleable.StatSheet.Portrait);

                    _buttons.Add(button);
                }
            }
        }

        public void SetCurrentPortrait(Battleable currentBattleable)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].Character == currentBattleable)
                {
                    _buttons[i].Grow();
                }
                else
                {
                    _buttons[i].Shrink();
                }
            }
        }

        private void ResetList()
        {
            if (_buttons.Count > 0)
            {
                foreach (InitiativeButton button in _buttons)
                {
                    Destroy(button.gameObject);
                }

                _buttons.Clear();
            }
        }
    }
}