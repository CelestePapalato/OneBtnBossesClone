using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class ObjectPoolData
    {
        public string tag;
        public GameObject objectToPool;
        public int quantity;
    }
    private class Pool
    {
        private List<GameObject> objects = new List<GameObject>();
        private GameObject product;

        public Pool(int quantity, GameObject objectToPool)
        {
            product = objectToPool;
            for (int i = 0; i < quantity; i++)
            {
                IncreasePool();
            }
        }

        private GameObject IncreasePool()
        {
            GameObject obj = Instantiate(product);
            obj.SetActive(false);
            objects.Add(obj);
            return obj;
        }

        public GameObject GetObject(Vector2 position, Quaternion rotation)
        {
            GameObject obj = null;
            foreach (GameObject tmp in objects)
            {
                if (!tmp.activeInHierarchy)
                {
                    obj = tmp;
                    break;
                }
            }
            if (!obj)
            {
                obj = IncreasePool();
            } 
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
    }

    public static ObjectPool Instance { get; private set; }

    [SerializeField]
    ObjectPoolData[] objectsToPool;

    Dictionary<string, Pool> poolData = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        foreach (ObjectPoolData data in objectsToPool)
        {
            if (!poolData.ContainsKey(data.tag))
            {
                poolData.Add(data.tag, new Pool(data.quantity, data.objectToPool));
            }
        }
    }

    public GameObject GetObject(string tag, Vector2 position, Quaternion rotation)
    {
        GameObject obj = null; 
        if (poolData.ContainsKey(tag))
        {
            obj = poolData[tag].GetObject(position, rotation);
        }
        return obj;
    }
}
