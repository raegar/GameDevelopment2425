using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using PatternLibrary;

public class SpatialSFXPool : Singleton<SpatialSFXPool>
{
    public GameObject objectToPool;
    public int poolSize = 32;
    public bool canGrow = true;
    public int maxPoolSize = 32;
    public int minPoolSize;

    private IObjectPool<GameObject> pool;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        poolSize -= audioSources.Length;
        maxPoolSize -= audioSources.Length;

        if (poolSize > maxPoolSize || minPoolSize > maxPoolSize)
        {
            poolSize = maxPoolSize;
            minPoolSize = maxPoolSize;
        }
        if (minPoolSize == 0)
        {
            minPoolSize = poolSize;
        }

        pool = new ObjectPool<GameObject>(CreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck: true, defaultCapacity: poolSize, maxSize: maxPoolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = CreatePooledObject();
            pool.Release(pooledObject);
        }
    }

    private GameObject CreatePooledObject()
    {
        GameObject pooledObject = Instantiate(objectToPool, transform.position, Quaternion.identity, transform);
        return pooledObject;
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetFromPool()
    {
        if (pool.Get() == null)
        {
            
        }
        return pool.Get();
    }

    public void ReturnToPool(GameObject obj)
    {
        pool.Release(obj);
    }

    public GameObject MoveSoundObjectToMe(GameObject obj)
    {
        GameObject pooledObject = GetFromPool();
        pooledObject.transform.position = obj.transform.position;
        SoundObject soundObject = pooledObject.GetComponent<SoundObject>();
        soundObject.linkedObject = obj;

        return pooledObject;
    }

    public IEnumerator ReturnToPoolAfterSound(GameObject obj, float delay)
    {
        SoundObject soundObject = obj.GetComponent<SoundObject>();
        yield return new WaitForSeconds(delay);
        soundObject.linkedObject = null;
        ReturnToPool(obj);
    }

    public void ReturnToPoolAfterSoundInstant(GameObject obj)
    {
        SoundObject soundObject = obj.GetComponent<SoundObject>();
        soundObject.linkedObject = null;
        ReturnToPool(obj);
    }
}
