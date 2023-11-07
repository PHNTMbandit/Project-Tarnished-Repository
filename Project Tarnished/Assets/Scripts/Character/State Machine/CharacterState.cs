using UnityEngine;

namespace ProjectTarnished.Character.StateMachine
{
    public abstract class CharacterState : ScriptableObject
    {
        [SerializeField]
        private string _stateAnimationName;

        public virtual void OnEnter(CharacterStateMachineController stateMachineController)
        {
            stateMachineController.Animator.SetBool(_stateAnimationName, true);
        }

        public virtual void OnExit(CharacterStateMachineController stateMachineController)
        {
            stateMachineController.Animator.SetBool(_stateAnimationName, false);
        }

        public virtual void OnUpdate(CharacterStateMachineController stateMachineController)
        {
        }

        public virtual void OnFixedUpdate(CharacterStateMachineController stateMachineController)
        {
        }
    }
}