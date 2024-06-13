using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;  // The prefab to be pooled
    public int poolSize = 5;   // The number of objects in the pool

    public List<GameObject> objectPool;

    void Start()
    {
        InitializeObjectPool();
    }

    void InitializeObjectPool()
    {
        objectPool = new List<GameObject>();

        // Find existing objects with the specified tag and add them to the pool
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag("Windmill");
        foreach (GameObject obj in existingObjects)
        {
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        // Instantiate additional objects to meet the pool size
        for (int i = 0; i < poolSize - existingObjects.Length; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    // Retrieve an object from the pool
    public GameObject GetObjectFromPool()
    {
        // Find an inactive object in the pool
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        // If no inactive object is found, expand the pool
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        objectPool.Add(newObj);

        return newObj;
    }

    // Deactivate an object and return it to the pool
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
