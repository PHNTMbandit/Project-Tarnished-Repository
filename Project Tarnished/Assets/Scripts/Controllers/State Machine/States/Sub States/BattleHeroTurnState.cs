using Pathfinding;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    [CreateAssetMenu(fileName = "Hero Turn State", menuName = "Project Tarnished/Game/State/Hero Turn")]
    public class BattleHeroTurnState : BattleState
    {
        private AIPath _heroAI;
        private CharacterMove _characterMove;
        private Commandable _currentHero;

        public override void OnEnter(GameStateMachineController stateMachineController)
        {
            base.OnEnter(stateMachineController);

            _heroAI = stateMachineController.GetCurrentTurn().GetComponent<AIPath>();
            _characterMove = stateMachineController.GetCurrentTurn().GetComponent<CharacterMove>();
            _currentHero = stateMachineController.GetCurrentTurn().GetComponent<Commandable>();

            stateMachineController.HeroController.ChangeHero(_currentHero);

            _characterMove.onMove += _characterMove.UseMoveSpeed;
            stateMachineController.InputReader.onClick += stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease += stateMachineController.HeroController.ReleaseSelect;

        }

        public override void OnExit(GameStateMachineController stateMachineController)
        {
            base.OnExit(stateMachineController);

            stateMachineController.PathLine.Clear();

            _characterMove.onMove -= _characterMove.UseMoveSpeed;
            stateMachineController.InputReader.onClick -= stateMachineController.HeroController.ClickSelect;
            stateMachineController.InputReader.onRelease -= stateMachineController.HeroController.ReleaseSelect;
        }

        public override void OnUpdate(GameStateMachineController stateMachineController)
        {
            base.OnUpdate(stateMachineController);

            stateMachineController.PathLine.Show(_heroAI, _characterMove);
            stateMachineController.NextTurnButton.SetInteractable(!_currentHero.IsRunningCommands());
        }
    }
}