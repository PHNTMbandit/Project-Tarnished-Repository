using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data
{
    [Serializable]
    public enum AttributeName
    {
        Agility,
        Attunement,
        Logic,
        Prescence,
        Resolve,
        Vigour,
    }

    [Serializable]
    public class Attribute
    {
        [HideLabel, ShowInInspector]
        public Stat Score
        {
            get => new(baseValue);
        }

        [HideLabel, ReadOnly]
        public AttributeName attributeName;

        [Range(0, 20)]
        public int baseValue = 10;

        public Attribute(AttributeName attributeName)
        {
            this.attributeName = attributeName;
        }
    }
}