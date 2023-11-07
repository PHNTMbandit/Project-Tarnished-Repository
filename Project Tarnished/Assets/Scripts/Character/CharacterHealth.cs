using ProjectLumina.Character;
using ProjectTarnished.Character;
using ProjectTarnished.Character.StateMachine;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Stats;
using ProjectTarnished.Data.Wounds;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Data
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(CharacterWounds))]
    [RequireComponent(typeof(CharacterStatSheet))]
    [RequireComponent(typeof(CharacterStateMachineController))]
    [AddComponentMenu("Character/Character Health")]
    public class CharacterHealth : MonoBehaviour
    {
        public Health Health
        {
            get => _statSheet.StatSheet.Health;
        }

        private Animator _animator;
        private CharacterAttributes _attributes;
        private CharacterStatSheet _statSheet;
        private CharacterWounds _wounds;
        private CharacterStateMachineController _stateMachineController;

        public UnityEvent onWoundPointsChanged;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _attributes = GetComponent<CharacterAttributes>();
            _statSheet = GetComponent<CharacterStatSheet>();
            _stateMachineController = GetComponent<CharacterStateMachineController>();
            _wounds = GetComponent<CharacterWounds>();
        }

        public void ChangeWoundPoints(AttributeName attackAttributeName, int attackRoll, Wound inflictableWound)
        {
            int damage = attackRoll - (int)_attributes.Attributes.GetAttribute(attackAttributeName).Score.Value;
            Health.CurrentWoundPoints += damage;

            ActivityLogController.Instance.AddActivityLog($"{gameObject.name} takes {damage} damage");

            if (Health.CurrentWoundPoints >= Health.MaxWoundPoints)
            {
                Health.CurrentWoundPoints = 0;

                _wounds.AddWound(inflictableWound);
            }

            onWoundPointsChanged?.Invoke();
        }

        public bool IsHit(int attackRoll, AttributeName attackAttributeName)
        {
            return attackRoll > (float)_attributes.Attributes.GetAttribute(attackAttributeName).Score.Value;
        }

        public bool IsCritical(int attackRoll, AttributeName attackAttributeName)
        {
            return attackRoll >= ((float)_attributes.Attributes.GetAttribute(attackAttributeName).Score.Value * 2);
        }

        public void ShowHitAnimation(Transform origin)
        {
            Vector2 direction = transform.position - origin.transform.position;
            _animator.SetFloat("hit direction x", Mathf.Clamp(direction.x, -1, 1));
            _animator.SetFloat("hit direction y", Mathf.Clamp(direction.y, -1, 1));

            _stateMachineController.SetState("Hit State");
        }
    }
}