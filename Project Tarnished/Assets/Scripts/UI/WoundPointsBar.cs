using ProjectTarnished.Controllers;
using ProjectTarnished.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class WoundPointsBar : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TextMeshProUGUI _amount;

        private CharacterHealth _health;

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
            if (_controller.CurrentHero.TryGetComponent(out CharacterHealth health))
            {
                if (health != null)
                {
                    health.onWoundPointsChanged.RemoveListener(UpdateUI);
                }

                _health = health;
                _health.onWoundPointsChanged.AddListener(UpdateUI); ;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _slider.maxValue = _health.Health.MaxWoundPoints;
            _slider.value = _health.Health.CurrentWoundPoints;
            _amount.SetText($"{_health.Health.CurrentWoundPoints}\n{_health.Health.MaxWoundPoints}");
        }
    }
}