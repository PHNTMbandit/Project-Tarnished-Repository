using ProjectTarnished.Controllers;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class HeroSelectList : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        private HeroSelectButton[] _buttons = new HeroSelectButton[4];

        private void Awake()
        {
            _buttons = GetComponentsInChildren<HeroSelectButton>();

            _controller.onHeroChange += GenerateList;
        }

        private void Start()
        {
            GenerateList();
        }

        private void GenerateList()
        {
            ResetList();

            for (int i = 0; i < _controller.Party.Length; i++)
            {
                _buttons[i].SetHero(_controller.Party[i]);
                _buttons[i].SetName(_controller.Party[i].name);
            }
        }

        private void ResetList()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].ResetButton();
            }
        }
    }
}