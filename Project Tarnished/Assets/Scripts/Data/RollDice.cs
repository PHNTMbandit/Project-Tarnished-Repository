using System;
using Random = UnityEngine.Random;

namespace ProjectTarnished.Data
{
    [Serializable]
    public enum DiceType
    {
        D4,
        D6,
        D8,
        D10,
        D12,
        D20
    }

    public static class RollDice
    {
        public static int Roll(DiceType diceType, int amount)
        {
            int roll = 0;

            for (int i = 0; i < amount; i++)
            {
                switch (diceType)
                {
                    case DiceType.D4:
                        roll += Random.Range(1, 4);
                        break;
                    case DiceType.D6:
                        roll += Random.Range(1, 6);
                        break;
                    case DiceType.D8:
                        roll += Random.Range(1, 8);
                        break;
                    case DiceType.D10:
                        roll += Random.Range(1, 10);
                        break;
                    case DiceType.D12:
                        roll += Random.Range(1, 12);
                        break;
                    case DiceType.D20:
                        roll += Random.Range(1, 20);
                        break;
                }
            }

            return roll;
        }
    }
}
