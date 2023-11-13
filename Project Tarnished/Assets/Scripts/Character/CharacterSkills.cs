using System;
using ProjectLumina.Character;
using ProjectTarnished.Data;
using ProjectTarnished.Data.Stats;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(CharacterStatSheet))]
    [AddComponentMenu("Character/Character Skills")]
    public class CharacterSkills : MonoBehaviour
    {
        private CharacterAttributes _attributes;
        private CharacterStatSheet _statSheet;

        private void Awake()
        {
            _attributes = GetComponent<CharacterAttributes>();
            _statSheet = GetComponent<CharacterStatSheet>();
        }

        public bool SkillCheck(Skill skill, int DC)
        {
            return (RollDice.Roll2D10() + GetSkill(skill).SkillLevel) >= DC;
        }

        public bool SkillCheck(string skillName, int DC, out int roll)
        {
            roll = RollDice.Roll2D10() + GetSkill(skillName).SkillLevel;
            return roll >= DC;
        }

        public Skill GetSkill(Skill skill)
        {
            return Array.Find(_statSheet.StatSheet.Skills, i => i == skill);
        }

        public Skill GetSkill(string skillName)
        {
            return Array.Find(_statSheet.StatSheet.Skills, i => i.SkillName == skillName);
        }
    }
}