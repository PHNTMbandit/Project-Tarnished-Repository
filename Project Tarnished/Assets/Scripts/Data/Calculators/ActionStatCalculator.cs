using ProjectTarnished.Character;

namespace ProjectTarnished.Data.Calculators
{
    public static class ActionStatCalculator
    {
        private static readonly float _baseActionStat = 1;
        private static readonly float _attributeCoefficient = 0.55f;
        private static readonly float _levelCoefficient = 0.35f;

        public static int GetActionStat(Attribute attribute, CharacterLevel level)
        {
            return (int)(_baseActionStat * (1 + (attribute.Score.Value - 10) * _attributeCoefficient) * (1 + level.Level.CurrentLevel * _levelCoefficient));
        }
    }
}