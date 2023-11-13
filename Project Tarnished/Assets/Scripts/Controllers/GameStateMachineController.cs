using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectLumina.Capabilities;
using ProjectTarnished.Camera;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers.StateMachine;
using ProjectTarnished.Input;
using ProjectTarnished.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Controllers
{
    public class GameStateMachineController : MonoBehaviour
    {
        #region Properties

        public GameStateMachine StateMachine { get; private set; }
        public List<Battleable> InitiativeOrder { get; private set; } = new();

        [field: BoxGroup("Controllers"), SerializeField]
        public CRPGCamera CRPGCamera { get; private set; }

        [field: BoxGroup("Controllers"), SerializeField]
        public HeroController HeroController { get; private set; }

        [field: BoxGroup("Controllers"), SerializeField]
        public InputReader InputReader { get; private set; }

        [field: BoxGroup("UI"), SerializeField]
        public NextTurnButton NextTurnButton { get; private set; }

        [field: BoxGroup("UI"), SerializeField]
        public MovementPathLine PathLine { get; private set; }

        [field: BoxGroup("UI"), SerializeField]
        public UIPanel[] BattleCanvasGroups { get; private set; }

        #endregion Properties

        #region Variables

        [BoxGroup("UI"), Range(0, 5), SerializeField]
        private float _battleStartTimer;

        [BoxGroup("States"), SerializeField]
        private GameState[] _gameStates;

        private bool _isBattleActive;
        private int _currentTurnIndex = -1;

        public UnityAction onInitiativeChanged;
        public UnityAction<Battleable> onTurnAdvanced;

        #endregion Variables

        #region Singleton

        public static GameStateMachineController Instance
        { get { return _instance; } }

        private static GameStateMachineController _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion Singleton

        #region Unity Callback Functions

        private void Start()
        {
            StateMachine = new(this);

            StateMachine.Initialise(GetState("Out Dialogue State"));
        }

        private void Update()
        {
            StateMachine.CurrentState.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.OnFixedUpdate(this);
        }

        #endregion Unity Callback Functions

        #region Initiative Functions

        public void AddToInitiative(Battleable battleable)
        {
            if (!InitiativeOrder.Contains(battleable))
            {
                InitiativeOrder.Add(battleable);

                if (battleable.TryGetComponent(out CharacterWounds characterWounds))
                {
                    characterWounds.onDead.AddListener(delegate { RemoveFromInitiative(battleable); });
                }
            }

            UpdateInitiative();

            if (_isBattleActive == false)
            {
                StartBattle();
            }

            onInitiativeChanged?.Invoke();
        }

        public void RemoveFromInitiative(Battleable battleable)
        {
            if (InitiativeOrder.Contains(battleable))
            {
                InitiativeOrder.Remove(battleable);
            }

            if (InitiativeOrder.Any(i => i.StatSheet.CharacterType == Data.CharacterType.NPC))
            {
                UpdateInitiative();
            }
            else
            {
                FinishBattle();
            }

            onInitiativeChanged?.Invoke();
        }

        public void UpdateInitiative()
        {
            InitiativeOrder = InitiativeOrder.OrderByDescending(i => i.CharacterAbilityPoints.CurrentAbilityPoints).ToList();

            onInitiativeChanged?.Invoke();
        }

        private void StartBattle()
        {
            _isBattleActive = true;
            _currentTurnIndex = -1;

            for (int i = 0; i < BattleCanvasGroups.Length; i++)
            {
                BattleCanvasGroups[i].Open();
            }

            Invoke(nameof(AdvanceTurn), _battleStartTimer);
        }

        private void FinishBattle()
        {
            _isBattleActive = false;
            SetState("In Dialogue State");
        }

        #endregion Initiative Functions

        #region Turn Functions

        public void AdvanceTurn()
        {
            _currentTurnIndex++;

            if (_currentTurnIndex >= InitiativeOrder.Count)
            {
                _currentTurnIndex = -1;

                SetState("Round Start State");
            }
            else
            {
                Battleable currentTurn = InitiativeOrder[_currentTurnIndex];

                if (currentTurn.gameObject.layer == LayerMask.NameToLayer("Hero"))
                {
                    SetState("Hero Turn State");
                }
                else
                {
                    SetState("Enemy Turn State");
                }

                StartCoroutine(StartTurn(currentTurn));

                onTurnAdvanced?.Invoke(currentTurn);
            }
        }

        private IEnumerator StartTurn(Battleable battleable)
        {
            CRPGCamera.SnapTo(battleable.transform);

            battleable.StartTurn();

            yield return new WaitUntil(() => battleable.IsCurrentTurn == false);

            AdvanceTurn();
        }

        public Battleable GetCurrentTurn()
        {
            return InitiativeOrder[_currentTurnIndex];
        }

        #endregion Turn Functions

        #region State Functions

        public void SetState(string stateName)
        {
            if (!IsState(stateName))
            {
                StateMachine.ChangeState(GetState(stateName));
            }
        }
        public void SetState(GameState state)
        {
            if (!IsState(state))
            {
                StateMachine.ChangeState(GetState(state));
            }
        }

        public GameState GetState(string stateName)
        {
            return Array.Find(_gameStates, i => i.name == stateName);
        }

        public GameState GetState(GameState state)
        {
            return Array.Find(_gameStates, i => i == state);
        }

        public bool IsState(string stateName)
        {
            return StateMachine.CurrentState == GetState(stateName);
        }

        public bool IsState(GameState state)
        {
            return StateMachine.CurrentState == state;
        }

        #endregion State Functions

        #region Dialogue Functions

        public void EnterDialogue(Transform actor)
        {
            StateMachine.ChangeState(GetState("In Dialogue State"));

            CRPGCamera.SnapTo(actor);
        }

        public void ExitDialogue(Transform actor)
        {
            StateMachine.ChangeState(GetState("Out Dialogue State"));

            CRPGCamera.SnapTo(actor);
        }

        #endregion
    }
}