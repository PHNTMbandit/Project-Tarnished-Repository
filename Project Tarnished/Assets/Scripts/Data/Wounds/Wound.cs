using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Data.Wounds
{
    [CreateAssetMenu(fileName = "New Wound", menuName = "Project Tarnished/Wound", order = 0)]
    public class Wound : ScriptableObject
    {
        [field: BoxGroup("Details"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite WoundSprite { get; private set; }

        [field: BoxGroup("Details"), SerializeField]
        public string WoundName { get; private set; }

        [field: BoxGroup("Details"), TextArea, SerializeField]
        public string WoundDescription { get; private set; }
    }
}