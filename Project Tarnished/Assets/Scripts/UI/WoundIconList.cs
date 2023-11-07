using System.Collections.Generic;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class WoundIconList : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private WoundIconSlot _templateSlot;

        [PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        private Sprite _emptySlotBorder, _fullSlotBorder;

        private readonly List<WoundIconSlot> _slots = new();

        private void Awake()
        {
            _templateSlot.gameObject.SetActive(false);
        }

        private void Start()
        {
            GenerateList();
        }

        public void GenerateList()
        {
            ResetList();

            var wounds = _controller.CurrentHero.GetComponent<CharacterWounds>();

            for (int i = 0; i < wounds.MaxWounds; i++)
            {
                WoundIconSlot button = Instantiate(_templateSlot.gameObject, _transform).GetComponent<WoundIconSlot>();
                button.gameObject.SetActive(true);
                button.ResetSlot(_emptySlotBorder);

                _slots.Add(button);
            }

            for (int i = 0; i < wounds.Wounds.Count; i++)
            {
                _slots[i].SetIcon(wounds.Wounds[i].WoundSprite, _fullSlotBorder);
            }
        }

        private void ResetList()
        {
            foreach (WoundIconSlot slot in _slots)
            {
                slot.ResetSlot(_emptySlotBorder);
            }
        }
    }
}