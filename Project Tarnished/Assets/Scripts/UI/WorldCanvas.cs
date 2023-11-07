using UnityEngine;

namespace ProjectTarnished.UI
{
    public class WorldCanvas : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;
            GetComponent<Canvas>().sortingLayerName = "UI";
        }
    }
}