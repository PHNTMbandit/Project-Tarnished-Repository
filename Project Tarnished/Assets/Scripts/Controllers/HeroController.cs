using System;
using ProjectTarnished.Camera;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Commands;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.Input;
using ProjectTarnished.UI;
using ProjectTarnished.UI.Cursor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Selectable = ProjectTarnished.Capabilities.Selectable;

namespace ProjectTarnished.Controllers
{
    public class HeroController : MonoBehaviour
    {
        #region Varibles

        public Commandable CurrentHero { get; private set; }
        public Ability SelectedAbility { get; private set; }

        [field: BoxGroup("Heroes"), ListDrawerSettings(ShowFoldout = false), SerializeField]
        public Commandable[] Party { get; private set; }

        [BoxGroup("Controllers"), SerializeField]
        private CRPGCamera _cameraController;

        [BoxGroup("UI"), SerializeField]
        private RectTransform _gameRectTransform;

        [BoxGroup("UI"), SerializeField]
        private MouseCursorSO _defaultMouseCursor;

        [BoxGroup("Input"), SerializeField]
        private InputReader _inputReader;

        [BoxGroup("Input"), SerializeField]
        private LayerMask _commandableLayerMask, _selectableLayerMask;

        private MouseCursorHandler _mouseCursorHandler;
        private Selectable _currenSelectable;
        private UnityEngine.Camera _camera;
        private ContactFilter2D _commandContactFilter, _selectContactFilter = new();

        public UnityAction onItemSelected;
        public UnityAction onHeroChange;

        #endregion Variables

        #region Unity Callback Functions

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _commandContactFilter.useTriggers = true;
            _commandContactFilter.SetLayerMask(_commandableLayerMask);
            _selectContactFilter.useTriggers = true;
            _selectContactFilter.SetLayerMask(_commandableLayerMask);

            _mouseCursorHandler = GetComponent<MouseCursorHandler>();

            CurrentHero = Party[0];
        }

        private void Update()
        {
            HoverSelect();
        }

        #endregion Unity Callback Functions

        #region Command Functions

        public void Command()
        {
            if (HasRaycastHit(_commandableLayerMask, out Collider2D hit, out Vector2 cursorWorldPosition))
            {
                if (hit != null && SelectedAbility != null)
                {
                    if (CurrentHero.TryGetComponent(out CharacterAbilityPoints characterAbilityPoints))
                    {
                        if (SelectedAbility.CanUseAbility(CurrentHero, hit.gameObject))
                        {
                            CurrentHero.ClearCommands();
                            SelectedAbility.UseAbility(CurrentHero, hit.gameObject);
                            CurrentHero.ExecuteCommands();

                            if (GameStateMachineController.Instance.IsState("Hero Turn State"))
                            {
                                characterAbilityPoints.UseAbilityPoints(SelectedAbility.AbilityPoints);
                            }
                        }
                    }
                }
                else
                {
                    if (CurrentHero.TryGetComponent(out CharacterMove characterMove))
                    {
                        CurrentHero.ClearCommands();
                        CurrentHero.AddCommand(new MoveCommand(characterMove, cursorWorldPosition));
                        CurrentHero.ExecuteCommands();

                        ClearSelectedAbility();
                    }
                }
            }
        }

        #endregion Command Functions

        #region Select Functions

        public void HoverSelect()
        {
            if (HasRaycastHit(_selectableLayerMask, out Collider2D hit, out Vector2 cursorWorldPosition))
            {
                if (hit != null)
                {
                    if (hit.gameObject.TryGetComponent(out Selectable selectable))
                    {
                        if (_currenSelectable != selectable)
                        {
                            _currenSelectable = selectable;
                            selectable.PointerEnter();
                        }
                    }
                }
                else if (_currenSelectable != null)
                {
                    _currenSelectable.PointerExit();
                    _currenSelectable = null;
                }
            }
        }

        public void ClickSelect()
        {
            if (HasRaycastHit(_selectableLayerMask, out Collider2D hit, out Vector2 cursorWorldPosition))
            {
                if (hit != null)
                {
                    if (hit.gameObject.TryGetComponent(out Selectable selectable))
                    {
                        selectable.PointerClick();
                    }
                }
            }
        }

        public void ReleaseSelect()
        {
            if (HasRaycastHit(_selectableLayerMask, out Collider2D hit, out Vector2 cursorWorldPosition))
            {
                if (hit != null)
                {
                    if (hit.gameObject.TryGetComponent(out Selectable selectable))
                    {
                        selectable.PointerRelease();
                    }
                }
            }
        }

        #endregion Select Functions

        #region Hero Functions

        public void ChangeHero(Commandable hero)
        {
            CurrentHero = Array.Find(Party, i => i == hero);

            _cameraController.SnapTo(CurrentHero.transform);

            onHeroChange?.Invoke();
        }

        #endregion Hero Functions

        #region Action Functions

        public void ClearSelectedAbility()
        {
            SelectedAbility = null;

            _mouseCursorHandler.SetCursor(_defaultMouseCursor);

            onItemSelected?.Invoke();
        }

        public void SetSelectedAbility(Ability ability)
        {
            if (ability != null)
            {
                SelectedAbility = ability;
            }

            onItemSelected?.Invoke();
        }

        #endregion

        #region Other Functions

        public bool HasRaycastHit(LayerMask layerMask, out Collider2D hit, out Vector2 cursorWorldPosition)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_gameRectTransform, _inputReader.PointerPosition))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_gameRectTransform, _inputReader.PointerPosition, null, out Vector2 mousePosition);
                Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
                worldPosition += new Vector3(20, 11.4f);
                cursorWorldPosition = worldPosition;
                hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, layerMask).collider;

                return true;
            }

            hit = null;
            cursorWorldPosition = Vector2.zero;
            return false;
        }

        #endregion Other Functions
    }
}