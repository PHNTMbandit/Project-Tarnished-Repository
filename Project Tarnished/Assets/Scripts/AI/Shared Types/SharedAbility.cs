using BehaviorDesigner.Runtime;
using ProjectTarnished.Data.Abilities;

namespace ProjectTarnished.AI.SharedTypes
{
    [System.Serializable]
    public class SharedAbility : SharedVariable<Ability>
    {
        public static implicit operator SharedAbility(Ability value) => new() { Value = value };
    }
}