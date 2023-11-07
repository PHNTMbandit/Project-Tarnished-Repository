using ProjectTarnished.Character;

namespace ProjectTarnished.Data.Calculators
{
    public static class MoveSpeedCalculator
    {
        private static readonly float _baseMoveSpeed = 10;
        private static readonly float _attributeCoefficient = 0.10f;
        private static readonly float _levelCoefficient = 0.05f;

        public static int GetMoveSpeed(Attribute attribute, CharacterLevel level)
        {
            return (int)(_baseMoveSpeed * (1 + (attribute.Score.Value - 10) * _attributeCoefficient) * (1 + level.Level.CurrentLevel * _levelCoefficient));
        }
    }
}