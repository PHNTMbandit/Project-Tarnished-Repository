using ProjectTarnished.Controllers;
using UnityEngine;

namespace ProjectTarnished.Factories
{
    public class FXFactory
    {
        public GameObject GetFXObject(string fxName, Transform user, Vector2 direction)
        {
            var FXObject = ObjectPoolController.Instance.GetPooledObject(fxName, user.position, user, true);
            Animator animator = FXObject.GetComponent<Animator>();
            animator.SetFloat("move x", Mathf.Clamp(direction.x, -1, 1));
            animator.SetFloat("move y", Mathf.Clamp(direction.y, -1, 1));

            return FXObject;
        }
    }
}