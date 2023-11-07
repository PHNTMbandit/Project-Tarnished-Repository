using System;
using ProjectTarnished.Data;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [AddComponentMenu("Character/Character Skills")]
    public class CharacterSkills : MonoBehaviour
    {
        [SerializeField]
        private Skill[] _skills;

        public Skill GetSkill(string skillName)
        {
            return Array.Find(_skills, i => i.SkillName == skillName);
        }
    }
}