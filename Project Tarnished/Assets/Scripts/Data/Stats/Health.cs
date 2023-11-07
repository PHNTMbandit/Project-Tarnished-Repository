using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Stats
{
    [Serializable, HideLabel]
    public class Health
    {
        public int CurrentWoundPoints
        {
            get => _currentWoundPoints;
            set => _currentWoundPoints = value <= 0 ? 0 : value >= _maxWoundPoints ? _maxWoundPoints : value;
        }

        public int MaxWoundPoints => _maxWoundPoints;

        [Range(0, 100), LabelWidth(125), SerializeField]
        private int _maxWoundPoints;

        [ProgressBar(0, "_maxWoundPoints", ColorGetter = "GetHealthBarColour"), LabelWidth(125), SerializeField]
        private int _currentWoundPoints;

        private Color GetHealthBarColour(float value)
        {
            return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / _maxWoundPoints, .5f));
        }
    }
}