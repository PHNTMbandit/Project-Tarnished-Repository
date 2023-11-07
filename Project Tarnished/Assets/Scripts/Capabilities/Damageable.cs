using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data;
using ProjectTarnished.Data.Wounds;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Capabilities
{
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(CharacterWounds))]
    [AddComponentMenu("Capabilities/Damageable")]
    public class Damageable : MonoBehaviour
    {
        private CharacterHealth _health;
        private CharacterWounds _wounds;

        public UnityEvent onDamaged;

        private void Awake()
        {
            _health = GetComponent<CharacterHealth>();
            _wounds = GetComponent<CharacterWounds>();
        }

        public void Damage(Transform origin, AttributeName attackAttributeName, int attackRoll, Wound inflictableWound)
        {
            if (_health.IsCritical(attackRoll, attackAttributeName))
            {
                ActivityLogController.Instance.AddActivityLog($"{gameObject.name} takes critical damage");

                _wounds.AddWound(inflictableWound);
                _health.ShowHitAnimation(origin);
            }
            else if (_health.IsHit(attackRoll, attackAttributeName))
            {
                _health.ChangeWoundPoints(attackAttributeName, attackRoll, inflictableWound);
                _health.ShowHitAnimation(origin);
            }
            else
            {
                ActivityLogController.Instance.AddActivityLog($"{gameObject.name} dodges attack");
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}