using Pathfinding;
using ProjectTarnished.Character;
using ProjectTarnished.Data.Calculators;
using ProjectTarnished.Input;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class MovementPathLine : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private AIPath _previewLinePath;

        [FoldoutGroup("References"), SerializeField]
        private Seeker _seeker;

        [FoldoutGroup("References"), SerializeField]
        private LineRenderer _lineRenderer;

        [FoldoutGroup("References"), SerializeField]
        private InputReader _inputReader;

        [FoldoutGroup("References"), SerializeField]
        private RectTransform _gameRectTransform;

        [FoldoutGroup("References"), SerializeField]
        private MovementBar _movementBarUI;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;

            Clear();
        }

        public void Show(AIPath character, CharacterMove move)
        {
            if (HasRaycastHit(out Vector2 cursorWorldPosition))
            {
                if (character.velocity == Vector3.zero || character.hasPath == false)
                {
                    ABPath path = ABPath.Construct(character.transform.position, cursorWorldPosition);
                    _seeker.StartPath(path);

                    SetTrail(_previewLinePath);
                    SetColour(move.CurrentMoveSpeed, (float)(move.CurrentMoveSpeed / GetCalculatedPathDistance()));
                    _movementBarUI.ShowPreviewSlider(true);
                    _movementBarUI.SetPreviewSlider(GetCalculatedPathDistance());
                }
            }
        }

        public void Clear()
        {
            _lineRenderer.positionCount = 0;
            _movementBarUI.ShowPreviewSlider(false);
        }

        private void SetColour(float currentSpeed, float index)
        {
            if (GetCalculatedPathDistance() < currentSpeed)
            {
                _lineRenderer.startColor = Color.white;
                _lineRenderer.endColor = Color.white;
            }
            else if (currentSpeed <= 0)
            {
                _lineRenderer.startColor = Color.red;
                _lineRenderer.endColor = Color.red;
            }
            else
            {
                var gradient = new Gradient();
                var colors = new GradientColorKey[2];
                colors[0] = new GradientColorKey(Color.white, index - 0.01f);
                colors[1] = new GradientColorKey(Color.red, index);

                var alphas = new GradientAlphaKey[2];
                alphas[0] = new GradientAlphaKey(1.0f, index - 0.01f);
                alphas[1] = new GradientAlphaKey(1.0f, index);

                gradient.mode = GradientMode.PerceptualBlend;
                gradient.SetKeys(colors, alphas);
                _lineRenderer.colorGradient = gradient;
            }
        }

        private void SetTrail(AIPath AIPath)
        {
            _lineRenderer.positionCount = AIPath.path.vectorPath.Count;

            for (int i = 0; i < AIPath.path.vectorPath.Count; i++)
            {
                _lineRenderer.SetPosition(i, AIPath.path.vectorPath[i]);
            }
        }

        public void SetMovementBarPreview()
        {
            _movementBarUI.SetPreviewSlider(GetCalculatedPathDistance());
        }

        public float GetCalculatedPathDistance()
        {
            return MoveDistanceCalculator.CalculatePathDistance(_previewLinePath.path.vectorPath);
        }

        public bool HasRaycastHit(out Vector2 cursorWorldPosition)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_gameRectTransform, _inputReader.PointerPosition))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_gameRectTransform, _inputReader.PointerPosition, null, out Vector2 mousePosition);
                Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
                worldPosition += new Vector3(20, 11.4f);
                cursorWorldPosition = worldPosition;

                return true;
            }

            cursorWorldPosition = Vector2.zero;
            return false;
        }
    }
}