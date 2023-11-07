using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data
{
    [Serializable]
    public enum SkillDifficultyLevel
    {
        Easy,
        Average,
        Hard,
    }

    [CreateAssetMenu(fileName = "Skill", menuName = "Project Tarnished/Skill", order = 2)]
    public class Skill : ScriptableObject
    {
        [field: SerializeField]
        public string SkillName { get; private set; }

        [field: SerializeField]
        public Attribute Attribute { get; private set; }

        [field: EnumToggleButtons, SerializeField]
        public SkillDifficultyLevel DifficultyLevel { get; private set; }

        [field: ToggleLeft, SerializeField]
        public bool CanUseDefault { get; private set; }

        [field: ShowIf("CanUseDefault"), Range(-6, -4), SerializeField]
        public int DefaultLevel { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }
    }
}