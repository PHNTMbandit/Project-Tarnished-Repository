using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    public abstract class PeaceState : GameState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            foreach (var UIPanel in stateMachineController.BattleCanvasGroups)
            {
                UIPanel.Close();
            }
        }
    }
}