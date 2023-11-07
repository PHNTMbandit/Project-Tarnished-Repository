using UnityEngine;

namespace ProjectTarnished.Character.StateMachine.States
{
    [CreateAssetMenu(fileName = "Move State", menuName = "Project Tarnished/Character/State/Move")]
    public class CharacterMoveState : CharacterState
    {
        public override void OnUpdate(CharacterStateMachineController stateMachineController)
        {
            base.OnUpdate(stateMachineController);

            stateMachineController.Move.UpdateMovingAnimation();

            if (stateMachineController.Move.IsMoving() == false)
            {
                stateMachineController.SetState("Idle State");
            }
        }
    }
}