using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using PatternLibrary;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
#endif

public class SpatialSFXPool : Singleton<SpatialSFXPool>
{
    public GameObject objectToPool;
    public int initialPoolSize = 32;
    public bool canGrow = true;
    public int maxPoolSize = 32;
    public int minPoolSize;

    private IObjectPool<GameObject> pool;
    [ReadOnly] [SerializeField] private int currentPoolSize;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        initialPoolSize -= audioSources.Length;
        maxPoolSize -= audioSources.Length;

        if (initialPoolSize > maxPoolSize || minPoolSize > maxPoolSize)
        {
            initialPoolSize = maxPoolSize;
            minPoolSize = maxPoolSize;
        }
        if (minPoolSize == 0)
        {
            minPoolSize = initialPoolSize;
        }

        pool = new ObjectPool<GameObject>(CreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck: true, defaultCapacity: initialPoolSize, maxSize: maxPoolSize);
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject pooledObject = CreatePooledObject();
            pool.Release(pooledObject);
        }

        DragDropSound[] dragDroppables = FindObjectsOfType<DragDropSound>();

        foreach (DragDropSound dragDroppable in dragDroppables)
        {
            if (dragDroppable.GetComponent<PlayOnEnable>())
            {
                dragDroppable.GetComponent<PlayOnEnable>().OnPoolCreated();
            }
        }

    }

    private GameObject CreatePooledObject()
    {
        GameObject pooledObject = Instantiate(objectToPool, transform.position, Quaternion.identity, transform);
        currentPoolSize++;
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
        currentPoolSize--;
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

    public void ReturnToPoolInstant(GameObject obj)
    {
        SoundObject soundObject = obj.GetComponent<SoundObject>();
        soundObject.linkedObject = null;
        ReturnToPool(obj);
    }
}


