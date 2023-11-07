using System;
using Sirenix.OdinInspector;

namespace ProjectTarnished.Data.Stats
{
    [Serializable, HideLabel]
    public class Attributes
    {
        [ListDrawerSettings(ShowFoldout = false)]
        public Attribute[] attributes =
        {
            new(AttributeName.Agility),
            new(AttributeName.Attunement),
            new(AttributeName.Logic),
            new(AttributeName.Prescence),
            new(AttributeName.Resolve),
            new(AttributeName.Vigour)
        };

        public Attribute GetAttribute(AttributeName attributeName)
        {
            return Array.Find(attributes, i => i.attributeName == attributeName);
        }

        public Attribute GetAttribute(string attributeName)
        {
            return Array.Find(attributes, i => i.attributeName.ToString() == attributeName);
        }

        public Attribute GetAttribute(int index)
        {
            return attributes[index];
        }
    }
}