using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class AttributeStatUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name, _value;

        public void SetName(string name)
        {
            _name.SetText(name);
        }

        public void SetValue(int amount)
        {
            _value.SetText(amount.ToString());
        }
    }
}