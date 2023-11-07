using ProjectTarnished.Capabilities;
using UnityEngine;

namespace ProjectTarnished.Interfaces
{
    public interface IAction
    {
        public bool CanUseAbility(Commandable user, GameObject target);
        public void UseAbility(Commandable user, GameObject target);
    }
}