using System.Collections;
using ProjectTarnished.Capabilities;
using ProjectTarnished.Character;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.Interfaces;
using UnityEngine;

namespace ProjectTarnished.Commands
{
    public class MeleeAttackCommand : ICommand
    {
        private readonly CharacterMeleeAttack _user;
        private readonly MeleeAttackAbility _meleeAttackAbility;
        private readonly Damageable _target;
        private bool _isAnimationRunning;

        public MeleeAttackCommand(CharacterMeleeAttack user, MeleeAttackAbility meleeAttackAbility, Damageable target)
        {
            _user = user;
            _meleeAttackAbility = meleeAttackAbility;
            _target = target;
        }

        public void Execute()
        {
            _user.MeleeAttack(_meleeAttackAbility, _target);
            _user.StartCoroutine(Countdown(_meleeAttackAbility.AttackDuration));
        }

        public bool IsFinished()
        {
            return _isAnimationRunning == false;
        }

        private IEnumerator Countdown(float duration)
        {
            _isAnimationRunning = true;
            float counter = duration;

            while (counter > 0.0f)
            {
                counter -= Time.deltaTime;

                yield return null;
            }

            _isAnimationRunning = false;
        }
    }
}