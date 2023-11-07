using ProjectTarnished.Input;
using ProjectTarnished.UI.Cursor;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class MouseCursorHandler : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        private MouseCursorSO _currentMouseCursor;

        private void Update()
        {
            if (_currentMouseCursor != null)
            {
                if (_inputReader.IsPointerDown)
                {
                    SetCursorTexture(MouseCursorType.Click);
                }
                else
                {
                    SetCursorTexture(MouseCursorType.Idle);
                }
            }
        }

        public void SetCursor(MouseCursorSO mouseCursor)
        {
            if (mouseCursor != _currentMouseCursor)
            {
                _currentMouseCursor = mouseCursor;
            }
        }

        private void SetCursorTexture(MouseCursorType mouseCursorType)
        {
            var mouseCursor = _currentMouseCursor.GetMouseCursor(mouseCursorType);
            UnityEngine.Cursor.SetCursor(mouseCursor.MouseCursorTexture, mouseCursor.MouseCursorHotspot, CursorMode.Auto);
        }
    }
}