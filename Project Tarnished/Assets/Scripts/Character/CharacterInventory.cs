using ProjectTarnished.Data.Stats;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Inventory")]
    public class CharacterInventory : MonoBehaviour
    {
        public Inventory Inventory
        {
            get => _statSheet.StatSheet.Inventory;
        }

        private CharacterStatSheet _statSheet;

        private void Awake()
        {
            _statSheet = GetComponent<CharacterStatSheet>();
        }
    }
}