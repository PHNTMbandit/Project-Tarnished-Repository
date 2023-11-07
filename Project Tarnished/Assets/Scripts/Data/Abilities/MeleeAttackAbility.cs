using System;
using System.Linq;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Character.StateMachine;
using ProjectTarnished.Commands;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Wounds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Abilities
{
    [CreateAssetMenu(fileName = "New Melee Attack", menuName = "Project Tarnished/Abilities/Attack/Melee Attack")]
    public class MeleeAttackAbility : Ability
    {
        [field: TabGroup("Attack"), Range(0, 10), SerializeField]
        public float AttackRange { get; private set; } = 1;

        [field: TabGroup("Attack"), Range(0, 100), SerializeField]
        public int BaseAttack { get; private set; }

        [field: TabGroup("Attack"), EnumToggleButtons, SerializeField]
        public AttributeName AttributeModifierName { get; private set; }

        [field: TabGroup("Wounds"), SerializeField]
        public Wound[] InflictableWounds { get; private set; }

        [field: TabGroup("Animation"), ToggleLeft, SerializeField]
        public bool UsesAttackFX { get; private set; }

        [field: TabGroup("Animation"), Range(0, 10), SerializeField]
        public float AttackDuration { get; private set; } = 0.5f;

        [field: ShowIf("UsesAttackFX"), TabGroup("Animation"), SerializeField]
        public ObjectPoolObject AttackFX { get; private set; }

        [field: TabGroup("Animation"), SerializeField]
        public CharacterState CharacterState { get; private set; }

        public override bool CanUseAbility(Commandable user, GameObject target)
        {
            if (user.TryGetComponent(out CharacterAbilityPoints characterAbilityPoints))
            {
                if ((characterAbilityPoints.CurrentAbilityPoints - AbilityPoints) >= 0)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(user.transform.position, AttackRange);

                    if (colliders.Any(i => i.gameObject == target))
                    {
                        return true;
                    }
                    else if (user.TryGetComponent(out CharacterMove characterMove))
                    {
                        float distance = Vector3.Distance(target.transform.position, user.transform.position);
                        return characterMove.CurrentMoveSpeed > (distance - AttackRange);
                    }
                }
            }

            return false;
        }

        public override void UseAbility(Commandable user, GameObject target)
        {
            if (user.TryGetComponent(out CharacterMove move))
            {
                Vector3 directionToPlayer = user.transform.position - target.transform.position;
                Vector2 targetPosition = target.transform.position + directionToPlayer.normalized * AttackRange;
                user.AddCommand(new MoveCommand(move, targetPosition));
            }

            if (user.TryGetComponent(out CharacterMeleeAttack characterMeleeAttack) && target.TryGetComponent(out Damageable damageable))
            {
                user.AddCommand(new MeleeAttackCommand(characterMeleeAttack, this, damageable));
            }
        }
    }
}