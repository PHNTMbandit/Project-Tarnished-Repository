using System.Collections;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.UI
{
    public class NumberCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _numberText;

        private Coroutine _C2T;
        private float _currentValue, _targetValue;

        private void Awake()
        {
            _numberText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _currentValue = float.Parse(_numberText.text);
            _targetValue = _currentValue;
        }

        public void SetText(string text)
        {
            _numberText.SetText(text);
        }

        public void AddValue(float value)
        {
            _targetValue += value;
            if (_C2T != null)
            {
                StopCoroutine(_C2T);
            }

            _C2T = StartCoroutine(CountTo(_targetValue));
        }

        public void SetValue(float target)
        {
            _targetValue = target;
            if (_C2T != null)
            {
                StopCoroutine(_C2T);
            }

            _C2T = StartCoroutine(CountTo(_targetValue));
        }

        private IEnumerator CountTo(float targetValue)
        {
            var rate = Mathf.Abs(targetValue - _currentValue) / 1;
            while (_currentValue != targetValue)
            {
                _currentValue = Mathf.MoveTowards(_currentValue, targetValue, rate * Time.deltaTime);
                _numberText.text = ((int)_currentValue).ToString();

                yield return null;
            }
        }
    }
}