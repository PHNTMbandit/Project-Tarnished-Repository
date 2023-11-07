using ProjectLumina.Capabilities;
using ProjectTarnished.Character;
using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "Round Start State", menuName = "Project Tarnished/Game/State/Round Start Turn")]
    public class BattleRoundStartState : BattleState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            foreach (Battleable character in stateMachineController.InitiativeOrder)
            {
                character.Reset();
            }

            stateMachineController.UpdateInitiative();
            stateMachineController.AdvanceTurn();
        }
    }
}