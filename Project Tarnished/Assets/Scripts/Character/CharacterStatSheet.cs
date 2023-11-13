using ProjectTarnished.Data.Stats;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [AddComponentMenu("Character/Character Stat Sheet")]
    public class CharacterStatSheet : MonoBehaviour
    {
        [field: SerializeField]
        public StatSheet StatSheet { get; private set; }

        private void Awake()
        {
            SetCharacterGameobjectName();
        }

        private void SetCharacterGameobjectName()
        {
            StatSheet = Instantiate(StatSheet);
            gameObject.name = StatSheet.CharacterFirstName;
        }
    }
}