using System.Collections.Generic;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Items;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class ItemList : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private ItemSlot _templateItemSlot;

        [SerializeField]
        private Transform _listTransform;

        private readonly List<ItemSlot> _slots = new();
        private CharacterInventory _inventory;

        private void Awake()
        {
            _templateItemSlot.gameObject.SetActive(false);

            _controller.onHeroChange += Initialise;
        }

        private void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_controller.CurrentHero.TryGetComponent(out CharacterInventory inventory))
            {
                if (inventory != null)
                {
                    inventory.onInventoryChanged -= GenerateList;
                }

                _inventory = inventory;
                _inventory.onInventoryChanged += GenerateList; ;

                GenerateList();
            }
        }

        public void GenerateList()
        {
            ResetList();

            for (int i = 0; i < _inventory.GetItems().Count; i++)
            {
                ItemSlot itemSlot = Instantiate(_templateItemSlot.gameObject, _listTransform).GetComponent<ItemSlot>();
                Item item = _inventory.GetItem(i);

                itemSlot.gameObject.SetActive(true);
                itemSlot.SetItem(item);
                itemSlot.SetIcon(item.ItemSprite);

                _slots.Add(itemSlot);
            }
        }

        private void ResetList()
        {
            if (_slots.Count > 0)
            {
                foreach (ItemSlot itemSlot in _slots)
                {
                    Destroy(itemSlot.gameObject);
                }

                _slots.Clear();
            }
        }
    }
}