using System;
using UnityEngine;

namespace ProjectTarnished.Character.StateMachine
{
    [RequireComponent(typeof(Animator))]
    public class CharacterStateMachineController : MonoBehaviour
    {
        #region Properties

        public Animator Animator { get; private set; }
        public CharacterMove Move { get; private set; }
        public CharacterStateMachine StateMachine { get; private set; }

        #endregion Properties

        #region Variables

        [SerializeField]
        private CharacterState[] _states;

        #endregion Variables

        #region Unity Callback Functions

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Move = GetComponent<CharacterMove>();

            StateMachine = new(this);
        }

        private void Start()
        {
            StateMachine.Initialise(GetState("Idle State"));
        }

        private void Update()
        {
            StateMachine.CurrentState.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.OnFixedUpdate(this);
        }

        #endregion Unity Callback Functions

        #region State Functions

        public void SetState(string stateName)
        {
            StateMachine.ChangeState(GetState(stateName));
        }
        public void SetState(CharacterState state)
        {
            StateMachine.ChangeState(GetState(state));
        }

        public CharacterState GetState(string stateName)
        {
            return Array.Find(_states, i => i.name == stateName);
        }

        public CharacterState GetState(CharacterState state)
        {
            return Array.Find(_states, i => i == state);
        }

        #endregion State Functions
    }
}