using ProjectTarnished.Capabilities;
using ProjectTarnished.Data.Abilities;
using ProjectTarnished.UI.Cursor;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace ProjectTarnished.Data.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Project Tarnished/Item")]
    public class Item : ScriptableObject
    {
        [field: BoxGroup("Details"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite ItemSprite { get; private set; }

        [field: BoxGroup("Details"), SerializeField]
        public string ItemName { get; private set; }

        [field: BoxGroup("Details"), TextArea, SerializeField]
        public string ItemDescription { get; private set; }

        [field: BoxGroup("Details"), SerializeField]
        public MouseCursorSO MouseCursor { get; private set; }

        [BoxGroup("Abilities"), SerializeField]
        private Ability[] _abilities;

        public bool CanUseItem(Commandable user, GameObject target)
        {
            if (_abilities.All(i => i.CanUseAbility(user, target) == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UseItem(Commandable user, GameObject target)
        {
            foreach (var action in _abilities)
            {
                action.UseAbility(user, target);
            }
        }

        public int GetItemAbilityPoints()
        {
            return _abilities.Sum(i => i.AbilityPoints);
        }

        public Ability[] GetItemAbilities()
        {
            return _abilities;
        }
    }
}