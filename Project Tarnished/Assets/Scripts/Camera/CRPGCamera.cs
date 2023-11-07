using System.Collections;
using Cinemachine;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Controllers;
using ProjectTarnished.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Camera
{
    public class CRPGCamera : MonoBehaviour
    {
        [BoxGroup("Controllers"), SerializeField]
        private HeroController _heroController;

        [BoxGroup("Controllers"), SerializeField]
        private InputReader _inputReader;

        [BoxGroup("Cameras"), SerializeField]
        private CinemachineVirtualCamera _gameCamera;

        [BoxGroup("Cameras"), SerializeField]
        private Collider2D _confiner;

        [BoxGroup("Pan Settings"), Range(0, 10f), SerializeField]
        private float _panSpeed, _panSmoothing;

        private CinemachineBrain _brain;
        private CinemachineVirtualCamera _camera;
        private Vector2 _panVelocity, _targetPosition;

        private void Start()
        {
            _brain = CinemachineCore.Instance.GetActiveBrain(0);
            _brain.m_CameraActivatedEvent.AddListener(ChangeCamera);
            _camera = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;

            _targetPosition = _camera.transform.position;
        }

        private void FixedUpdate()
        {
            Pan();
        }

        public void Unlock()
        {
            _gameCamera.VirtualCameraGameObject.transform.position = _camera.transform.position;
            _camera.Priority = 0;
            _gameCamera.Priority = 1;
        }

        public void LockOn(Transform target)
        {
            if (target.TryGetComponent(out CameraSnappable cameraSnappable))
            {
                _camera.Priority = 0;
                cameraSnappable.Snap(target);
            }
        }

        public void SnapTo(Transform target)
        {
            StartCoroutine(Snap(target));
        }

        private void Pan()
        {
            _targetPosition = _camera.transform.position + new Vector3(_inputReader.Pan.x, _inputReader.Pan.y, 0f) * _panSpeed;
            Vector3 clampedPosition = ClampToConfinerBounds(_targetPosition);
            _camera.transform.position = Vector2.SmoothDamp(_camera.transform.position, clampedPosition, ref _panVelocity, _panSmoothing);
        }

        private IEnumerator Snap(Transform target)
        {
            LockOn(target);

            yield return new WaitForSeconds(_brain.m_DefaultBlend.m_Time);
            yield return new WaitUntil(() => _inputReader.Pan != Vector2.zero);

            Unlock();
        }

        private void ChangeCamera(ICinemachineCamera newCamera, ICinemachineCamera oldCamera)
        {
            _camera = newCamera as CinemachineVirtualCamera;
            _targetPosition = _camera.transform.position;
        }

        private Vector3 ClampToConfinerBounds(Vector3 position)
        {
            Vector3 clampedPosition = position;

            if (_confiner != null)
            {
                Bounds confinerBounds = _confiner.bounds;

                float cameraHeight = 2f * _gameCamera.m_Lens.OrthographicSize;
                float cameraWidth = cameraHeight * _gameCamera.m_Lens.Aspect;
                Vector3 cameraHalfSize = new(cameraWidth / 2f, cameraHeight / 2f, 0f);

                clampedPosition.x = Mathf.Clamp(clampedPosition.x, confinerBounds.min.x + cameraHalfSize.x, confinerBounds.max.x - cameraHalfSize.x);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, confinerBounds.min.y + cameraHalfSize.y, confinerBounds.max.y - cameraHalfSize.y);
            }

            return clampedPosition;
        }
    }
}