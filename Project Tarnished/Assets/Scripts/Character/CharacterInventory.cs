using System.Collections.Generic;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.Data.Items;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Inventory")]
    public class CharacterInventory : MonoBehaviour
    {
        private CharacterStatSheet _statSheet;

        public UnityAction onInventoryChanged;

        private void Awake()
        {
            _statSheet = GetComponent<CharacterStatSheet>();
        }

        public void AddItem(Item item)
        {
            _statSheet.StatSheet.Inventory.Add(item);

            onInventoryChanged?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            if (_statSheet.StatSheet.Inventory.Contains(item))
            {
                _statSheet.StatSheet.Inventory.Remove(item);

                onInventoryChanged?.Invoke();
            }
        }

        public Item GetItem(int index)
        {
            return _statSheet.StatSheet.Inventory[index];
        }

        public Item GetItem(Item item)
        {
            return _statSheet.StatSheet.Inventory.Find(i => i == item);
        }

        public List<Item> GetItems()
        {
            return _statSheet.StatSheet.Inventory;
        }

        public List<Ability> GetInventoryAbilities()
        {
            List<Ability> abilities = new();

            foreach (var item in _statSheet.StatSheet.Inventory)
            {
                abilities.AddRange(item.GetItemAbilities());
            }

            return abilities;
        }
    }
}