using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Capabilities
{
    [AddComponentMenu("Capabilities/Selectable")]
    public class Selectable : MonoBehaviour
    {
        public UnityEvent onPointerEnter, onPointerExit, onPointerClick, onPointerRelease;

        public void PointerEnter()
        {
            onPointerEnter?.Invoke();
        }

        public void PointerExit()
        {
            onPointerExit?.Invoke();
        }

        public void PointerClick()
        {
            onPointerClick?.Invoke();
        }

        public void PointerRelease()
        {
            onPointerRelease?.Invoke();
        }
    }
}