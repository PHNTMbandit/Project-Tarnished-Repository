using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectTarnished.Input
{
    [CreateAssetMenu(fileName = "Input Handler", menuName = "Project Tarnished/Input/Input Handler", order = 1)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions
    {
        public GameControls GameControls { get; private set; }
        public Vector2 MoveInput { get; private set; }
        public Vector2 Pan { get; private set; }
        public Vector2 PointerPosition { get; private set; }
        public bool IsPointerDown { get; private set; }

        public UnityAction onClick, onContinue, onLockOn, onRelease;

        private void OnEnable()
        {
            if (GameControls == null)
            {
                GameControls = new GameControls();
                GameControls.Gameplay.AddCallbacks(this);
                GameControls.Gameplay.AddCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void OnContinue(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onContinue?.Invoke();
            }
        }

        public void OnLockOn(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onLockOn?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnPan(InputAction.CallbackContext context)
        {
            Pan = context.ReadValue<Vector2>();
        }

        public void OnPointerClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsPointerDown = false;

                onRelease?.Invoke();
            }
            else if (context.started)
            {
                IsPointerDown = true;

                onClick?.Invoke();
            }
        }

        public void OnPointerPosition(InputAction.CallbackContext context)
        {
            PointerPosition = context.ReadValue<Vector2>();
        }

        public void EnableUIInput()
        {
            GameControls.UI.Enable();
            GameControls.Gameplay.Disable();
        }

        public void EnableGameplayInput()
        {
            GameControls.UI.Disable();
            GameControls.Gameplay.Enable();
        }

        public void EnableAllInput()
        {
            GameControls.Gameplay.Disable();
            GameControls.UI.Disable();
        }

        public void DisableAllInput()
        {
            GameControls.Gameplay.Disable();
            GameControls.UI.Disable();
        }
    }
}