using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Stats
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Project Tarnished/Skill", order = 2)]
    public class Skill : ScriptableObject
    {
        [field: SerializeField]
        public string SkillName { get; private set; }

        [field: EnumToggleButtons, SerializeField]
        public AttributeName Attribute { get; private set; }

        [field: Range(0, 99), SerializeField]
        public int SkillLevel { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }
    }
}
