using UnityEngine;

namespace ProjectTarnished.Controllers
{
    public class ObjectPoolObject : MonoBehaviour
    {
        public void Disable()
        {
            gameObject.SetActive(false);
            transform.SetParent(ObjectPoolController.Instance.transform);
        }
    }
}