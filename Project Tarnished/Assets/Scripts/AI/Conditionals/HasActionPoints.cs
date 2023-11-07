using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using ProjectTarnished.Character;
using UnityEngine;

namespace ProjectTarnished.AI.Capabilities
{
    [TaskCategory("Character")]
    public class HasActionPoints : Conditional
    {
        [SerializeField]
        private SharedInt _abilityPoints;

        private CharacterAbilityPoints _characterAbilityPoints;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterAbilityPoints = GetComponent<CharacterAbilityPoints>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_characterAbilityPoints.CurrentAbilityPoints <= _abilityPoints.Value)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}