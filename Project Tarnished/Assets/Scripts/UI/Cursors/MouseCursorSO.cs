using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.UI.Cursor
{
    [Serializable]
    public enum MouseCursorType
    {
        Idle,
        Click,
        Red,
        Green,
    }

    [Serializable]
    public class MouseCursor
    {
        [field: EnumPaging, SerializeField]
        public MouseCursorType MouseCursorType { get; private set; }

        [field: SerializeField]
        public Texture2D MouseCursorTexture { get; private set; }

        [field: SerializeField]
        public Vector2 MouseCursorHotspot { get; private set; }
    }

    [CreateAssetMenu(fileName = "New Mouse Cursor", menuName = "Project Tarnished/Mouse Cursor", order = 0)]
    public class MouseCursorSO : ScriptableObject
    {
        [SerializeField]
        private MouseCursor[] _mouseCursors;

        public MouseCursor GetMouseCursor(MouseCursorType mouseCursorType)
        {
            return Array.Find(_mouseCursors, i => i.MouseCursorType == mouseCursorType);
        }
    }
}