using ProjectLumina.Character;
using ProjectTarnished.Character;

namespace ProjectTarnished.Data.Calculators
{
    public static class AttackCalculator
    {
        public static int GetAttackRoll(
            AttributeName attributeName,
            CharacterAttributes attributes,
            CharacterLevel level,
            int baseAttack
        )
        {
            Stat attack =
                new(
                    ActionStatCalculator.GetActionStat(
                        attributes.GetAttribute(attributeName),
                        level
                    ) + baseAttack
                );
            int attackRoll = (int)(attack.Value + RollDice.Roll(DiceType.D10, 2));

            return attackRoll;
        }
    }
}
