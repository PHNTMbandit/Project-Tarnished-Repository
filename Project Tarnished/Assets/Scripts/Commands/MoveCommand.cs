using ProjectTarnished.Character;
using ProjectTarnished.Interfaces;
using UnityEngine;

namespace ProjectTarnished.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly CharacterMove _characterMove;
        private Vector2 _destination;

        public MoveCommand(CharacterMove characterMove, Vector2 destination)
        {
            _characterMove = characterMove;
            _destination = destination;
        }

        public void Execute()
        {
            _characterMove.Move(_destination);
        }

        public bool IsFinished()
        {
            return _characterMove.HasArrived();
        }
    }
}