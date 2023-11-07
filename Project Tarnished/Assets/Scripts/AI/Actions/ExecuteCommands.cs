using BehaviorDesigner.Runtime.Tasks;
using ProjectTarnished.Capabilities;

namespace ProjectTarnished.AI.Actions
{
    [TaskCategory("Battle/Utilities")]
    public class ExecuteCommands : Action
    {
        private Commandable _commandable;

        public override void OnAwake()
        {
            base.OnAwake();

            _commandable = GetComponent<Commandable>();
        }

        public override void OnStart()
        {
            base.OnStart();

            _commandable.ExecuteCommands();
        }

        public override TaskStatus OnUpdate()
        {
            return _commandable.IsRunningCommands() ? TaskStatus.Running : TaskStatus.Success;
        }
    }
}