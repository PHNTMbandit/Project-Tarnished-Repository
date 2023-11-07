using UnityEngine;

namespace ProjectTarnished.Data
{
    public static class RollDice
    {
        public static int Roll2D10()
        {
            return RollD10() + RollD10();
        }

        private static int RollD10()
        {
            return Random.Range(1, 10);
        }
    }
}