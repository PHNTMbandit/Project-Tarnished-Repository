using System.Collections.Generic;
using ProjectTarnished.Controllers;
using ProjectTarnished.Data.Wounds;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Character
{
    [AddComponentMenu("Character/Character Wounds")]
    public class CharacterWounds : MonoBehaviour
    {
        [ShowInInspector]
        public List<Wound> Wounds { get; private set; } = new();

        [field: Range(0, 10), SerializeField]
        public int MaxWounds { get; private set; }

        [Space]
        public UnityEvent onWoundsChanged;

        public UnityEvent onDead;

        public void AddWound(Wound wound)
        {
            if (Wounds.Count < MaxWounds)
            {
                Wounds.Add(wound);

                ActivityLogController.Instance.AddActivityLog($"{gameObject.name} recieves a {wound.WoundName}");

                onWoundsChanged?.Invoke();
            }

            if (Wounds.Count >= MaxWounds)
            {
                ActivityLogController.Instance.AddActivityLog($"{gameObject.name} dies");

                onDead?.Invoke();
            }
        }

        public void RemoveWound(Wound wound)
        {
            if (Wounds.Contains(wound))
            {
                Wounds.Remove(wound);
            }
        }
    }
}