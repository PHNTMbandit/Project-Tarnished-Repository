using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.Capabilities;

namespace ProjectTarnished.AI.Actions
{
    [TaskCategory("Battle/Utilities")]
    public class NextTurn : Action
    {
        private Battleable _battleable;

        public override void OnAwake()
        {
            base.OnAwake();

            _battleable = GetComponent<Battleable>();
        }

        public override void OnStart()
        {
            base.OnStart();

            _battleable.FinishTurn();
        }
    }
}