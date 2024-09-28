using System.Collections.Generic;
using UnityEngine;

// When using this class you must AddComponent to your game object on start and invoke "Init" method:
/*
 *   private ObjectPool pool;
 *   private void Start()
    {
        pool = gameObject.AddComponent(typeof(ObjectPool)) as ObjectPool;
        pool.Init(poolSize, prefab, true);
    }

    Example:
    Create Scriptable object for SingleObjectPoolData and attach it to bulletsPoolData
    [Header("Object Pool")]
    public SingleObjectPoolData bulletsPoolData;
    private ObjectPool bulletsPool;

    private void Start()
    {
    bulletsPool = gameObject.AddComponent<ObjectPool>();
    bulletsPool.Init(bulletsPoolData.pool.poolSize, bulletsPoolData.pool.prefab);
    }
     void SpawnBullet()
    {
     bulletsPool.GetObject(bulletSpawnPosition.position);
    }
 */
public class ObjectPool : MonoBehaviour
{
    private GameObject prefab;
    private List<GameObject> pool;
    private GameObject poolHolder;
    private bool isSetParent;

    public void Init(int poolSize, GameObject prefab, bool isSetParent = false)
    {
        this.prefab = prefab;
        this.isSetParent = isSetParent;
        pool = new List<GameObject>();
        if (!isSetParent)
        {
            poolHolder = new GameObject($"{prefab.name} poolHolder");
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            HandleParent(obj);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void HandleParent(GameObject obj)
    {
        if (isSetParent)
        {
            obj.transform.parent = transform;
        }
        else
        {
            obj.transform.parent = poolHolder.transform;
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Expand the pool if necessary
        GameObject newObj = Instantiate(prefab);
        HandleParent(newObj);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
    
    public GameObject GetObject(Vector3 spawnPoint)
    {
        foreach (GameObject obj in pool)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                obj.transform.position = spawnPoint;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // Expand the pool if necessary
        GameObject newObj = Instantiate(prefab, spawnPoint, transform.rotation);
        HandleParent(newObj);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public GameObject GetObject(Vector3 spawnPoint, Quaternion rotation)
    {
        foreach (GameObject obj in pool)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                obj.transform.position = spawnPoint;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // Expand the pool if necessary
        GameObject newObj = Instantiate(prefab, spawnPoint, rotation);
        HandleParent(newObj);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public string GetPrefabName()
    {
        return prefab.name;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
