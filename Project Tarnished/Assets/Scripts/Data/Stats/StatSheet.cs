using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Stats
{


    [CreateAssetMenu(fileName = "New Stat Sheet", menuName = "Project Tarnished/Character/Stat Sheet", order = 0)]
    public class StatSheet : ScriptableObject
    {
        [field: TabGroup("Information"), SerializeField]
        public string CharacterFirstName { get; private set; }

        [field: TabGroup("Information"), SerializeField]
        public string CharacterLastName { get; private set; }

        [field: TabGroup("Information"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite Portrait { get; private set; }

        [field: TabGroup("Information"), EnumToggleButtons, SerializeField]
        public CharacterType CharacterType { get; private set; }

        [field: TabGroup("Level"), SerializeField]
        public Level Level { get; private set; }

        [field: TabGroup("Attributes"), SerializeField]
        public Attributes Attributes { get; private set; }

        [field: TabGroup("Health"), SerializeField]
        public Health Health { get; private set; }

        [field: TabGroup("Inventory"), SerializeField]
        public Inventory Inventory { get; private set; }

        public void SetName(string firstName, string lastName)
        {
            CharacterFirstName = firstName;
            CharacterLastName = lastName;
        }

        public void SetAttributes(Attributes attributes)
        {
            Attributes = attributes;
        }
    }
}