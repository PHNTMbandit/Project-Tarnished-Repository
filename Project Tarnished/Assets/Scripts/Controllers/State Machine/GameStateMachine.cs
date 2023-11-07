namespace ProjectTarnished.Controllers.StateMachine
{
    public class GameStateMachine
    {
        public GameState CurrentState { get; private set; }

        private readonly GameStateMachineController _stateMachineController;

        public GameStateMachine(GameStateMachineController stateMachineController)
        {
            _stateMachineController = stateMachineController;
        }

        public void Initialise(GameState startingState)
        {
            CurrentState = startingState;
            startingState.OnEnter(_stateMachineController);
        }

        public void ChangeState(GameState newState)
        {
            CurrentState.OnExit(_stateMachineController);

            CurrentState = newState;
            newState.OnEnter(_stateMachineController);
        }
    }
}