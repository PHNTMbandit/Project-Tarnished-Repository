using ProjectLumina.Character;
using ProjectTarnished.Character;
using UnityEngine;

namespace ProjectTarnished.Data.Calculators
{
    public static class ActionPointsCalculator
    {
        private static readonly float _baseActionPoints = 2;
        private static readonly float _attributeCoefficient = 0.55f;
        private static readonly float _levelCoefficient = 0.35f;

        public static int GetActionPoints(CharacterAttributes attributes, CharacterLevel level)
        {
            return (int)(_baseActionPoints * (1 + (attributes.Attributes.GetAttribute(AttributeName.Agility).Score.Value - 10) * _attributeCoefficient) * (1 + level.Level.CurrentLevel * _levelCoefficient));
        }
    }
}