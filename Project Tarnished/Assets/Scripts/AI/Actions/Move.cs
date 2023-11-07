using BehaviorDesigner.Runtime.Tasks;
using ProjectTarnished.AI.SharedTypes;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Commands;
using UnityEngine;

namespace ProjectTarnished.AI.Actions
{
    [TaskCategory("Battle/Misc")]
    public class Move : Action
    {
        public SharedSensor _sensor;

        private CharacterMove _characterMove;
        private Commandable _commandable;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterMove = GetComponent<CharacterMove>();
            _commandable = GetComponent<Commandable>();
        }

        public override void OnStart()
        {
            base.OnStart();

            GameObject target = _sensor.Value.GetNearestDetection();

            if (target != null)
            {
                _commandable.AddCommand(new MoveCommand(_characterMove, target.transform.position));
            }
        }
    }
}