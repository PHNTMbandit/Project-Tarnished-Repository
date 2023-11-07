using System;
using System.Collections.Generic;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.Data.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Data.Stats
{
    [Serializable, HideLabel]
    public class Inventory
    {
        [TableList(AlwaysExpanded = true), SerializeField]
        private List<Item> _items;

        public UnityAction onInventoryChanged;

        public void AddItem(Item item)
        {
            _items.Add(item);

            onInventoryChanged?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);

                onInventoryChanged?.Invoke();
            }
        }

        public Item GetItem(int index)
        {
            return _items[index];
        }

        public Item GetItem(Item item)
        {
            return _items.Find(i => i == item);
        }

        public List<Item> GetItems()
        {
            return _items;
        }

        public List<Ability> GetInventoryAbilities()
        {
            List<Ability> abilities = new();

            foreach (var item in _items)
            {
                abilities.AddRange(item.GetItemAbilities());
            }

            return abilities;
        }
    }
}