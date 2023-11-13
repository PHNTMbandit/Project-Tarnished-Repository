using ProjectTarnished.Character;
using ProjectTarnished.Data;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Attributes")]
    public class CharacterAttributes : MonoBehaviour
    {
        private CharacterStatSheet _characterStatSheet;

        public UnityAction onAttributesChanged;

        private void Awake()
        {
            _characterStatSheet = GetComponent<CharacterStatSheet>();
        }

        public Attribute GetAttribute(AttributeName attributeName)
        {
            return System.Array.Find(_characterStatSheet.StatSheet.attributes, i => i.attributeName == attributeName);
        }

        public Attribute GetAttribute(string attributeName)
        {
            return System.Array.Find(_characterStatSheet.StatSheet.attributes, i => i.attributeName.ToString() == attributeName);
        }

        public Attribute GetAttribute(int index)
        {
            return _characterStatSheet.StatSheet.attributes[index];
        }
    }
}