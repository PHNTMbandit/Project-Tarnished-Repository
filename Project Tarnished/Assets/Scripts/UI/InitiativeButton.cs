using DG.Tweening;
using ProjectLumina.Capabilities;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTarnished.UI
{
    public class InitiativeButton : MonoBehaviour
    {
        public Battleable Character { get; private set; }

        [SerializeField]
        private Image _portrait;

        public void SetCharacter(Battleable character)
        {
            Character = character;
        }

        public void SetPortrait(Sprite sprite)
        {
            _portrait.sprite = sprite;
        }

        public void Grow()
        {
            transform.DOScale(new Vector3(1.3f, 1.3f, 1), 0.5f);
        }

        public void Shrink()
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }
}