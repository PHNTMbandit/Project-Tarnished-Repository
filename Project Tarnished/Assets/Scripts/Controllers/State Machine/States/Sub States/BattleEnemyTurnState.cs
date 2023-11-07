using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "Enemy Turn State", menuName = "Project Tarnished/Game/State/Enemy Turn")]
    public class BattleEnemyTurnState : BattleState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            stateMachineController.NextTurnButton.SetInteractable(false);
        }
    }
}