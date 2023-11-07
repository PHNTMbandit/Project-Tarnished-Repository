using PixelCrushers;
using UnityEngine;

namespace ProjectTarnished.Player.Input
{
    public class InputRegistration : MonoBehaviour
    {
        protected static bool isRegistered = false;

        private GameControls _controls;
        private bool _didIRegister = false;

        private void Awake()
        {
            _controls = new GameControls();
        }

        private void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                _didIRegister = true;
                _controls.Enable();

                InputDeviceManager.RegisterInputAction("Continue", _controls.Gameplay.Continue);
            }
        }

        private void OnDisable()
        {
            if (_didIRegister)
            {
                isRegistered = false;
                _didIRegister = false;
                _controls.Disable();

                InputDeviceManager.UnregisterInputAction("Continue");
            }
        }
    }
}