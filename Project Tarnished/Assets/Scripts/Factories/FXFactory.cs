using ProjectTarnished.Controllers;
using UnityEngine;

namespace ProjectTarnished.Factories
{
    public class FXFactory
    {
        public Animator GetFXObject(string fxName, Transform user, Vector2 direction)
        {
            Animator FXObject = ObjectPoolController.Instance.GetPooledObject<Animator>(
                fxName,
                user.position,
                Quaternion.identity,
                user,
                true
            );

            FXObject.SetFloat("move x", Mathf.Clamp(direction.x, -1, 1));
            FXObject.SetFloat("move y", Mathf.Clamp(direction.y, -1, 1));

            return FXObject;
        }
    }
}
