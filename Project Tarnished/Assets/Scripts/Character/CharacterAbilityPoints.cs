using ProjectLumina.Character;
using ProjectTarnished.Data;
using ProjectTarnished.Data.Calculators;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(CharacterLevel))]
    [AddComponentMenu("Character/Character Action Points")]
    public class CharacterAbilityPoints : MonoBehaviour
    {
        public Stat AbilityPoints
        {
            get => new(ActionPointsCalculator.GetActionPoints(_attributes, _level));
        }

        public int CurrentAbilityPoints
        {
            get => _currentAbilityPoints;
            set => _currentAbilityPoints = (int)(value <= 0 ? 0 : value >= AbilityPoints.Value ? AbilityPoints.Value : value);
        }

        private CharacterAttributes _attributes;
        private CharacterLevel _level;
        private int _currentAbilityPoints;

        public UnityAction onUseAbilityPoints;

        private void Awake()
        {
            _attributes = GetComponent<CharacterAttributes>();
            _level = GetComponent<CharacterLevel>();

            ReplenishAbilityPoints();
        }

        public void UseAbilityPoints(int amount)
        {
            CurrentAbilityPoints -= amount;

            onUseAbilityPoints?.Invoke();
        }

        public void ReplenishAbilityPoints()
        {
            CurrentAbilityPoints = (int)AbilityPoints.Value;
        }
    }
}