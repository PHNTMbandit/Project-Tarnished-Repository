using UnityEngine;

namespace ProjectTarnished.Character.StateMachine.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Project Tarnished/Character/State/Idle")]
    public class CharacterIdleState : CharacterState
    {
        public override void OnUpdate(CharacterStateMachineController stateMachineController)
        {
            base.OnUpdate(stateMachineController);

            if (stateMachineController.Move.IsMoving())
            {
                stateMachineController.SetState("Move State");
            }
        }
    }
}