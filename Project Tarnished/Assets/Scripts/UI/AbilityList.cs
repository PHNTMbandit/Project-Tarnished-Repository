using System.Collections.Generic;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Abilities;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class AbilityList : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private AbilitySlot _templateAbilitySlot;

        [SerializeField]
        private Transform _listTransform;

        private CharacterAbilities _abilities;
        private readonly List<AbilitySlot> _slots = new();

        private void Awake()
        {
            _templateAbilitySlot.gameObject.SetActive(false);

            _controller.onHeroChange += Initialise;
        }

        private void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_controller.CurrentHero.TryGetComponent(out CharacterAbilities abilities))
            {
                if (abilities != null)
                {
                    // abilities.Abilities.onInventoryChanged -= GenerateList;
                }

                _abilities = abilities;
                // _abilities.Abilities.onInventoryChanged += GenerateList; ;

                GenerateList();
            }
        }

        public void GenerateList()
        {
            ResetList();

            for (int i = 0; i < _abilities.Abilities.Count; i++)
            {
                AbilitySlot abilitySlot = Instantiate(_templateAbilitySlot.gameObject, _listTransform).GetComponent<AbilitySlot>();
                Ability ability = _abilities.Abilities[i];

                abilitySlot.gameObject.SetActive(true);
                abilitySlot.SetAbility(ability);
                abilitySlot.SetIcon(ability.AbilitySprite);

                _slots.Add(abilitySlot);
            }
        }

        private void ResetList()
        {
            if (_slots.Count > 0)
            {
                foreach (AbilitySlot abilitySlot in _slots)
                {
                    Destroy(abilitySlot.gameObject);
                }

                _slots.Clear();
            }
        }
    }
}