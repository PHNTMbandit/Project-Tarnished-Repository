using Cinemachine;
using UnityEngine;

namespace ProjectTarnished.Capabilities
{
    [AddComponentMenu("Capabilities/Camera Snappable")]
    public class CameraSnappable : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _camera;

        public void Snap(Transform target)
        {
            _camera.VirtualCameraGameObject.transform.position = _camera.transform.position;
            _camera.Priority = 1;
            _camera.Follow = target;
        }
    }
}