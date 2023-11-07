using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class MovementBar : MonoBehaviour
    {
        [SerializeField]
        private HeroController _controller;

        [SerializeField]
        private Slider _previewSlider, _slider;

        [SerializeField]
        private TextMeshProUGUI _amount;

        private CharacterMove _move;

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
            if (_controller.CurrentHero.TryGetComponent(out CharacterMove move))
            {
                if (move != null)
                {
                    move.onMove -= UpdateUI;
                }

                _move = move;
                _move.onMove += UpdateUI;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _slider.maxValue = _move.MoveSpeed.Value;
            _slider.value = _move.CurrentMoveSpeed;
            _amount.SetText($"{_move.CurrentMoveSpeed:F2}\n{_move.MoveSpeed.Value:F2}");
        }

        public void ShowPreviewSlider(bool show)
        {
            _previewSlider.gameObject.SetActive(show);
        }

        public void SetPreviewSlider(float previewValue)
        {
            _previewSlider.maxValue = _move.MoveSpeed.Value;
            _previewSlider.value = Mathf.Clamp(_move.MoveSpeed.Value - previewValue, 0, _move.CurrentMoveSpeed);
            _amount.SetText($"{Mathf.Clamp(previewValue, 0, _move.CurrentMoveSpeed):F2}\n{_move.MoveSpeed.Value:F2}");
        }
    }
}