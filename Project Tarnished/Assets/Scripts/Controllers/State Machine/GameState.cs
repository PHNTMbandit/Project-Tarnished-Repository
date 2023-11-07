using UnityEngine;

namespace ProjectTarnished.Controllers.StateMachine
{
    public abstract class GameState : ScriptableObject
    {
        public virtual void OnEnter(GameStateMachineController stateMachineController)
        {
        }

        public virtual void OnExit(GameStateMachineController stateMachineController)
        {
        }

        public virtual void OnUpdate(GameStateMachineController stateMachineController)
        {
        }

        public virtual void OnFixedUpdate(GameStateMachineController stateMachineController)
        {
        }
    }
}