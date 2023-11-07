using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Data.Stats
{
    [Serializable, HideLabel]
    public class Level
    {
        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value <= 0 ? 0 : value >= _maxLevel ? _maxLevel : value;
        }

        [Range(1, 99), SerializeField]
        private int _maxLevel = 99;

        [ProgressBar(1, "_maxLevel"), SerializeField]
        private int _currentLevel = 1;

        [field: Space, ProgressBar(0, "RequiredXP"), ReadOnly, ShowInInspector]
        public int CurrentXP { get; private set; }

        [ReadOnly, ShowInInspector]
        public int RequiredXP
        {
            get => _baseXP * _currentLevel;
        }

        private readonly int _baseXP = 100;

        public UnityAction onXPGain, onLevelUp;

        public void AddXP(int xpAmount)
        {
            CurrentXP += xpAmount;

            while (CurrentXP >= RequiredXP && _currentLevel < 99)
            {
                LevelUp();
            }

            onXPGain?.Invoke();
        }

        public void LevelUp()
        {
            _currentLevel++;
            CurrentXP -= RequiredXP;

            onLevelUp?.Invoke();
        }
    }
}