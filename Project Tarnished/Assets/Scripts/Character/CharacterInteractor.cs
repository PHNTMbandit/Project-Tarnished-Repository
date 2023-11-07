using Micosmo.SensorToolkit;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Character
{
    [AddComponentMenu("Character/Character Interactor")]
    public class CharacterInteractor : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private InputReader _inputReader;

        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        public UnityEvent<Interactable> onInteractableDetected;
        public UnityEvent onInteractableLost, onInteract;

        private void OnEnable()
        {
            _sensor.OnDetected.AddListener(OnInteractableDetected);
            _sensor.OnLostDetection.AddListener(OnInteractableLost);
        }

        private void OnDisable()
        {
            _sensor.OnDetected.RemoveListener(OnInteractableDetected);
            _sensor.OnLostDetection.RemoveListener(OnInteractableLost);
        }

        public void OnInteract()
        {
            if (_sensor.GetNearestDetection() != null)
            {
                _sensor.GetNearestComponent<Interactable>().Interact();
            }
        }

        public void OnInteractableDetected(GameObject gameObject, Sensor sensor)
        {
            if (gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnDetected();

                onInteractableDetected?.Invoke(interactable);
            }
        }

        public void OnInteractableLost(GameObject gameObject, Sensor sensor)
        {
            if (gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnLost();

                onInteractableLost?.Invoke();
            }
        }
    }
}