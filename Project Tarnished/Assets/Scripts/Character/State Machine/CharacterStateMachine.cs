namespace ProjectTarnished.Character.StateMachine
{
    public class CharacterStateMachine
    {
        public CharacterState CurrentState { get; private set; }

        private readonly CharacterStateMachineController _stateMachineController;

        public CharacterStateMachine(CharacterStateMachineController stateMachineController)
        {
            _stateMachineController = stateMachineController;
        }

        public void Initialise(CharacterState startingState)
        {
            CurrentState = startingState;
            startingState.OnEnter(_stateMachineController);
        }

        public void ChangeState(CharacterState newState)
        {
            CurrentState.OnExit(_stateMachineController);

            CurrentState = newState;
            newState.OnEnter(_stateMachineController);
        }
    }
}