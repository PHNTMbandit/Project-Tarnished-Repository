using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "Out Dialogue State", menuName = "Project Tarnished/Game/State/Out Dialogue State")]
    public class PeaceOutDialogueState : PeaceState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            stateMachineController.InputReader.onClick += stateMachineController.HeroController.Command;
            stateMachineController.InputReader.onClick += stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease += stateMachineController.HeroController.ReleaseSelect;

            DialogueManager.instance.conversationStarted += stateMachineController.EnterDialogue;
        }

        public override void OnExit(GameStateMachineController stateMachineController)
        {
            base.OnExit(stateMachineController);

            DialogueManager.instance.conversationStarted -= stateMachineController.EnterDialogue;
        }
    }
}