using BehaviorDesigner.Runtime.Tasks;
using ProjectTarnished.AI.SharedTypes;
using ProjectTarnished.Capabilities;
using UnityEngine;

namespace ProjectTarnished.AI.Actions
{

    [TaskCategory("Character")]
    public class CanMeleeAttack : Conditional
    {
        public SharedAbility ability;
        public SharedSensor sensor;

        private Commandable _commandable;

        public override void OnAwake()
        {
            base.OnAwake();

            _commandable = GetComponent<Commandable>();
        }

        public override TaskStatus OnUpdate()
        {
            GameObject target = sensor.Value.GetNearestDetection();

            if (target != null)
            {
                return ability.Value.CanUseAbility(_commandable, target) ? TaskStatus.Success : TaskStatus.Failure;
            }

            return TaskStatus.Failure;
        }
    }
}