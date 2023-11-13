using ProjectTarnished.Data;
using ProjectTarnished.Data.Stats;
using UnityEngine;

namespace ProjectTarnished.Factories
{
    public class StatSheetBuilder
    {
        private readonly StatSheet _characterStatSheet;

        public StatSheetBuilder()
        {
            ScriptableObject.Instantiate(_characterStatSheet);
        }

        public StatSheet Build()
        {
            return _characterStatSheet;
        }

        public StatSheetBuilder SetName(string firstName, string lastName)
        {
            _characterStatSheet.SetName(firstName, lastName);
            return this;
        }

        public StatSheetBuilder SetAttributes(Attribute[] attributes)
        {
            _characterStatSheet.SetAttributes(attributes);
            return this;
        }
    }
}