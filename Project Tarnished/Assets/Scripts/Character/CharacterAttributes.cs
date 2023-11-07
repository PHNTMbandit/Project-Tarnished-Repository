using ProjectTarnished.Character;
using ProjectTarnished.Data.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Attributes")]
    public class CharacterAttributes : MonoBehaviour
    {
        public Attributes Attributes
        {
            get => _characterStatSheet.StatSheet.Attributes;
        }

        private CharacterStatSheet _characterStatSheet;

        public UnityAction onAttributesChanged;

        private void Awake()
        {
            _characterStatSheet = GetComponent<CharacterStatSheet>();
        }
    }
}