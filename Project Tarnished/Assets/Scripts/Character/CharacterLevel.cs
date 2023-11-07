using ProjectTarnished.Data.Stats;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Level")]
    public class CharacterLevel : MonoBehaviour
    {
        public Level Level
        {
            get => _statSheet.StatSheet.Level;
        }

        private CharacterStatSheet _statSheet;

        private void Awake()
        {
            _statSheet = GetComponent<CharacterStatSheet>();
        }
    }
}