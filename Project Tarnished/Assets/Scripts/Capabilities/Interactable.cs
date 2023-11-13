using ProjectTarnished.Character;
using ProjectTarnished.Commands;
using ProjectTarnished.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Capabilities
{
    [AddComponentMenu("Capabilities/Interactable")]
    public class Interactable : MonoBehaviour, ICommandable
    {
        [field: SerializeField]
        public string InteractText { get; private set; }

        public UnityEvent onDetected, onInteracted, onLost;

        public void OnDetected()
        {
            onDetected?.Invoke();
        }

        public void Interact()
        {
            onInteracted?.Invoke();
        }

        public void OnLost()
        {
            onLost?.Invoke();
        }

        public void AddCommands(Commandable commandable)
        {
            if (commandable.TryGetComponent(out CharacterMove characterMove))
            {
                commandable.AddCommand(new MoveCommand(characterMove, transform.position));
                commandable.AddCommand(new InteractCommand(this));
            }
        }
    }
}