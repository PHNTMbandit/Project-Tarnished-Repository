using ProjectLumina.Character;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character.StateMachine;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.Data.Calculators;
using ProjectTarnished.Data.Wounds;
using ProjectTarnished.Factories;
using UnityEngine;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterStateMachineController))]
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(CharacterLevel))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : MonoBehaviour
    {
        private Animator _animator;
        private CharacterStateMachineController _stateMachineController;
        private CharacterAttributes _attributes;
        private CharacterLevel _level;
        private readonly FXFactory _FXFactory = new();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _attributes = GetComponent<CharacterAttributes>();
            _level = GetComponent<CharacterLevel>();
            _stateMachineController = GetComponent<CharacterStateMachineController>();
        }

        public void MeleeAttack(MeleeAttackAbility action, Damageable target)
        {
            var attackRoll = AttackCalculator.GetAttackRoll(action.AttributeModifierName, _attributes, _level, action.BaseAttack);
            Wound wound = action.InflictableWounds[Random.Range(0, action.InflictableWounds.Length)];
            target.Damage(transform, action.AttributeModifierName, attackRoll, wound);

            Vector2 direction = target.transform.position - transform.position;
            _animator.SetFloat("attack direction x", Mathf.Clamp(direction.x, -1, 1));
            _animator.SetFloat("attack direction y", Mathf.Clamp(direction.y, -1, 1));

            _stateMachineController.SetState(action.CharacterState);

            if (action.UsesAttackFX)
            {
                _FXFactory.GetFXObject(action.AttackFX.name, transform, direction);
            }
        }
    }
}