using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "Peace State", menuName = "Project Tarnished/Game/State/Peace")]
    public class PeaceState : GameState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            foreach (var UIPanel in stateMachineController.BattleCanvasGroups)
            {
                UIPanel.Close();
            }

            stateMachineController.InputReader.onClick += stateMachineController.HeroController.Command;
            stateMachineController.InputReader.onClick += stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease += stateMachineController.HeroController.ReleaseSelect;

        }

        public override void OnExit(GameStateMachineController stateMachineController)
        {
            base.OnExit(stateMachineController);

            stateMachineController.InputReader.onClick -= stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease -= stateMachineController.HeroController.ReleaseSelect;
        }
    }
}