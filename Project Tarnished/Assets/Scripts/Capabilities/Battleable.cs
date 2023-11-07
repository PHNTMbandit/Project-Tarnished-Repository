using System.Collections.Generic;
using Micosmo.SensorToolkit;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Stats;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(CharacterAbilityPoints))]
    [AddComponentMenu("Capabilities/Battleable")]
    public class Battleable : MonoBehaviour
    {
        #region Properties

        public bool IsCurrentTurn { get; private set; }
        public CharacterAbilityPoints CharacterAbilityPoints { get; private set; }

        [field: SerializeField]
        public StatSheet StatSheet { get; private set; }

        #endregion Properties

        #region Variables

        [BoxGroup("Settings"), ToggleLeft, SerializeField]
        private bool _autoTrigger;

        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        public UnityEvent onEnterBattle, onTurnStart, onTurnFinished, onRoundEnd;

        #endregion Variables

        #region Unity Callback Functions

        private void OnEnable()
        {
            _sensor.OnDetected.AddListener(delegate { if (_autoTrigger && GameStateMachineController.Instance.IsState("Peace State")) EnterBattle(); });
        }

        private void OnDisable()
        {
            _sensor.OnDetected.RemoveAllListeners();
        }

        private void Awake()
        {
            CharacterAbilityPoints = GetComponent<CharacterAbilityPoints>();
        }

        #endregion Unity Callback Functions

        #region Battle Trigger Functions

        public void EnterBattle()
        {
            if (!GameStateMachineController.Instance.InitiativeOrder.Contains(this))
            {
                GameStateMachineController.Instance.AddToInitiative(this);

                foreach (var battleable in _sensor.GetDetectedComponents(new List<Battleable>()))
                {
                    battleable.EnterBattle();
                }

                onEnterBattle?.Invoke();
            }
        }

        #endregion Battle Trigger Functions

        #region Battle Functions

        public void StartTurn()
        {
            IsCurrentTurn = true;

            onTurnStart?.Invoke();
        }

        [Button]
        public void FinishTurn()
        {
            IsCurrentTurn = false;

            onTurnFinished?.Invoke();
        }

        public void Reset()
        {
            onRoundEnd?.Invoke();
        }

        #endregion Battle Functions
    }
}