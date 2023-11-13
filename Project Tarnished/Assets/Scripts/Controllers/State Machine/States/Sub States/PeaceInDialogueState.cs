using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "In Dialogue State", menuName = "Project Tarnished/Game/State/In Dialogue State")]
    public class PeaceInDialogueState : PeaceState
    {
        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);
            Debug.Log("in dialogue");

            stateMachineController.InputReader.onClick -= stateMachineController.HeroController.Command;
            stateMachineController.InputReader.onClick -= stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease -= stateMachineController.HeroController.ReleaseSelect;

            DialogueManager.instance.conversationEnded += stateMachineController.ExitDialogue;
        }

        public override void OnExit(GameStateMachineController stateMachineController)
        {
            base.OnExit(stateMachineController);

            DialogueManager.instance.conversationEnded -= stateMachineController.ExitDialogue;
        }
    }
}