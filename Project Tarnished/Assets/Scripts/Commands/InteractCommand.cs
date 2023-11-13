using ProjectTarnished.Capabilities;
using ProjectTarnished.Interfaces;

namespace ProjectTarnished.Commands
{
    public class InteractCommand : ICommand
    {
        private readonly Interactable _interactable;

        public InteractCommand(Interactable interactable)
        {
            _interactable = interactable;
        }

        public void Execute()
        {
            _interactable.Interact();
        }

        public bool IsFinished()
        {
            return true;
        }
    }
}