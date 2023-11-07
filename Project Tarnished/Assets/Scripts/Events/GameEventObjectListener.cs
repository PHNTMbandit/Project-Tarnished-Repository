using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Events
{
    public class GameEventObjectListener : MonoBehaviour
    {
        [TextArea, SerializeField]
        private string _eventDescription;

        [Space, SerializeField]
        private GameEventObject _event;

        [Space, SerializeField]
        private UnityEvent<object> _responses;

        private void OnEnable()
        {
            if (_event != null)
            {
                RegisterListener();
            }
        }

        private void OnDisable()
        {
            if (_event != null)
            {
                UnregisterListener();
            }
        }

        public void RegisterListener()
        {
            _event.RegisterListener(this);
        }

        public void UnregisterListener()
        {
            _event.UnregisterListener(this);
        }

        public void SetEvent(GameEventObject gameEvent)
        {
            _event = gameEvent;
        }

        public void OnEventRaised(object data)
        {
            _responses?.Invoke(data);
        }
    }
}