using ProjectTarnished.Character;
using ProjectTarnished.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private CharacterHealth _characterHealth;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private bool _alwaysVisible;

        private void Awake()
        {
            _characterHealth.onWoundPointsChanged.AddListener(UpdateUI);

            UpdateUI();
        }

        private void UpdateUI()
        {
            _slider.maxValue = _characterHealth.Health.MaxWoundPoints;
            _slider.value = _characterHealth.Health.CurrentWoundPoints;

            if (_text != null)
            {
                _text.SetText($"{_characterHealth.Health.CurrentWoundPoints}/{_characterHealth.Health.MaxWoundPoints}");
            }

            if (_alwaysVisible == false)
            {
                if (_characterHealth.Health.CurrentWoundPoints >= _characterHealth.Health.MaxWoundPoints || _characterHealth.Health.CurrentWoundPoints <= 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}