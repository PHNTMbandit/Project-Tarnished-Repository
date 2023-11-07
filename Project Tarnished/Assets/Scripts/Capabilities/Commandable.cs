using System.Collections;
using System.Collections.Generic;
using ProjectTarnished.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Capabilities
{
    [AddComponentMenu("Capabilities/Commandable")]
    public class Commandable : MonoBehaviour
    {
        [ShowInInspector]
        private readonly Queue<ICommand> _commandQueue = new();
        private bool _isRunning;

        public void AddCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
        }

        public void ExecuteCommands()
        {
            StopCoroutine(RunCommands());
            StartCoroutine(RunCommands());
        }

        public void CancelCommands()
        {
            StopCoroutine(RunCommands());
            ClearCommands();
        }

        public void ClearCommands()
        {
            _commandQueue.Clear();
        }

        private IEnumerator RunCommands()
        {
            _isRunning = true;

            while (_commandQueue.Count > 0)
            {
                ICommand command = _commandQueue.Dequeue();
                command.Execute();

                yield return new WaitUntil(command.IsFinished);
            }

            _isRunning = false;
        }

        public bool IsRunningCommands()
        {
            return _isRunning;
        }
    }
}