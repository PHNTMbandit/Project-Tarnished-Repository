using ProjectLumina.Character;
using ProjectTarnished.Controllers;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class AttributesListUI : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private AttributeStatUI[] _stats;

        private CharacterAttributes _attributes;

        private void Awake()
        {
            _controller.onHeroChange += Initialise;
        }

        private void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_controller.CurrentHero.TryGetComponent(out CharacterAttributes attributes))
            {
                if (attributes != null)
                {
                    attributes.onAttributesChanged -= UpdateUI;
                }

                _attributes = attributes;
                _attributes.onAttributesChanged += UpdateUI; ;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < _stats.Length; i++)
            {
                _stats[i].SetName(_attributes.GetAttribute(i).attributeName.ToString());
                _stats[i].SetValue((int)_attributes.GetAttribute(i).Score.Value);
            }
        }
    }
}