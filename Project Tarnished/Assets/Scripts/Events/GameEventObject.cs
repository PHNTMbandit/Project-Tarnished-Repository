using System.Collections.Generic;
using UnityEngine;

namespace ProjectTarnished.Events
{
    [CreateAssetMenu(fileName = "New Object Game Event", menuName = "Project Tarnished/Event/Game Event Object", order = 0)]
    public class GameEventObject : ScriptableObject
    {
        private readonly List<GameEventObjectListener> _listeners = new();

        public void Trigger(object data)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(GameEventObjectListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventObjectListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}