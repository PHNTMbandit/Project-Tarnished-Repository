using System.Text;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace ProjectTarnished.Controllers
{
    public class ActivityLogController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProTypewriterEffect _text;

        private readonly StringBuilder _stringBuilder = new();

        #region Singleton

        public static ActivityLogController Instance
        { get { return _instance; } }

        private static ActivityLogController _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion Singleton

        public void AddActivityLog(string text)
        {
            _stringBuilder.AppendLine(text);
            _text.PlayText(_stringBuilder.ToString(), _stringBuilder.Length - text.Length);
        }
    }
}