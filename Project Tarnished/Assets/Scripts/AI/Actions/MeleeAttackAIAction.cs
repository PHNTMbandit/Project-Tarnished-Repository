using BehaviorDesigner.Runtime.Tasks;
using ProjectTarnished.AI.SharedTypes;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using UnityEngine;

namespace ProjectTarnished.AI.Actions
{
    [TaskCategory("Battle/Utilities")]
    public class MeleeAttackAIAction : Action
    {
        public SharedAbility _ability;
        public SharedSensor _sensor;

        private CharacterAbilityPoints _abilityPoints;
        private Commandable _commandable;

        public override void OnAwake()
        {
            base.OnAwake();

            _abilityPoints = GetComponent<CharacterAbilityPoints>();
            _commandable = GetComponent<Commandable>();
        }

        public override void OnStart()
        {
            base.OnStart();

            GameObject target = _sensor.Value.GetNearestDetection();

            if (target != null)
            {
                if (_ability.Value.CanUseAbility(_commandable, target))
                {
                    _ability.Value.UseAbility(_commandable, target);
                    _abilityPoints.UseAbilityPoints(_ability.Value.AbilityPoints);
                }
            }
        }
    }
}