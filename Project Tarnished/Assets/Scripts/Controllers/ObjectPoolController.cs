using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectTarnished.Controllers
{
    [Serializable]
    public class Pool
    {
        public GameObject prefab;

        [Range(0, 1000)]
        public int size;
    }

    public class ObjectPoolController : MonoBehaviour
    {
        [TableList]
        public List<Pool> pools;

        public Dictionary<string, Queue<GameObject>> poolDictionary;

        #region Singleton

        public static ObjectPoolController Instance
        {
            get { return _instance; }
        }

        private static ObjectPoolController _instance;

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

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.prefab.name, objectPool);
            }
        }

        public T GetPooledObject<T>(string tag, Vector3 position)
            where T : Behaviour
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, Quaternion.identity);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn.GetComponent<T>();
        }

        public T GetPooledObject<T>(string tag, Vector3 position, Quaternion rotation)
            where T : Behaviour
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn.GetComponent<T>();
        }

        public T GetPooledObject<T>(
            string tag,
            Vector3 position,
            Quaternion rotation,
            Transform parent
        )
            where T : Behaviour
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.transform.SetParent(parent);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn.GetComponent<T>();
        }

        public T GetPooledObject<T>(
            string tag,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            bool worldPositionStays
        )
            where T : Behaviour
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.transform.SetParent(parent, worldPositionStays);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn.GetComponent<T>();
        }

        [Button("Sort Lists")]
        public void SortLists()
        {
            pools = pools.OrderBy(i => i.prefab.name.ToString()).ToList();
        }
    }
}
