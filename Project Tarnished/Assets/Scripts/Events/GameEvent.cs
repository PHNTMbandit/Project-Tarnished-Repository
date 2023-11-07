using System.Collections.Generic;
using UnityEngine;

namespace ProjectTarnished.Events
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Project Tarnished/Event/Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new();

        public void Trigger()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}