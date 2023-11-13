using System.Collections.Generic;
using ProjectTarnished.Data.Abilities;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterInventory))]
    [AddComponentMenu("Character/Character Abilities")]
    public class CharacterAbilities : MonoBehaviour
    {
        public List<Ability> Abilities
        {
            get => new(GetAllAbilities());
        }

        [SerializeField]
        private Ability[] _characterAbilities;

        private CharacterInventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<CharacterInventory>();
        }

        private List<Ability> GetAllAbilities()
        {
            List<Ability> allAbilities = new();

            allAbilities.AddRange(_inventory.GetInventoryAbilities());
            allAbilities.AddRange(_characterAbilities);

            return allAbilities;
        }
    }
}