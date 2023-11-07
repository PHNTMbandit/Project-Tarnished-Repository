using ProjectTarnished.Capabilities;
using ProjectTarnished.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Abilities
{
    public abstract class Ability : ScriptableObject, IAction
    {
        [field: BoxGroup("Ability"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite AbilitySprite { get; private set; }

        [field: TabGroup("Ability"), Range(0, 50), SerializeField]
        public int AbilityPoints { get; private set; }

        public abstract bool CanUseAbility(Commandable user, GameObject target);
        public abstract void UseAbility(Commandable user, GameObject target);
    }
}