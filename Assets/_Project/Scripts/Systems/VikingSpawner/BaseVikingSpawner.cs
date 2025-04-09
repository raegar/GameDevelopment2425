using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVikingSpawner : MonoBehaviour
{
    public static BaseVikingSpawner Instance { get; private set; }

    [Header("Location Settings")]
    public Transform[] spawnPoints;
    public Transform initialSpawn;

    [Header("Viking Settings")]
    [SerializeField] protected GameObject vikingPrefab;
    [SerializeField] protected int minCount = 1;
    [SerializeField] protected int maxCount = 3;

    [Header("Misc Settings")]
    [SerializeField] protected int initialVikingCount = 3;
    [SerializeField] protected float minimumVikingSpace = 0.5f;
    [Tooltip("Helps with not spawning vikings inside eachother")][SerializeField] private float xOffset = 1;
    [Tooltip("Helps with not spawning vikings inside eachother")][SerializeField] private float zOffset = 1;

    protected bool spawning = true;

    protected abstract void AnnounceSpawner();
    protected void Spawn(Transform transform, int count)
    {
        Transform[] transformsUsed = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            if (PopulationManager.Instance.populationCount < PopulationManager.Instance.currentMaxPopulation)
            {
                bool tooClose = true;
                transformsUsed[i] = OffsetTransform(transform);

                // check if transforms are too close too eachother

                if (tooClose)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        foreach (Transform t in transformsUsed)
                        {
                            if (Vector3.Distance(t.position, transformsUsed[i].position) < minimumVikingSpace)
                            {
                                transformsUsed[i] = OffsetTransform(transform);
                                break;
                            }
                            tooClose = false;
                        }
                    }
                }
                GameObject viking = Instantiate(vikingPrefab, transformsUsed[i].position, Quaternion.identity);
                Debug.Log($"Viking {viking.name} spawned at {transformsUsed[i].position}", viking);
            }
        }
    }

    protected void Spawn() // random spawn
    {
        Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)], Random.Range(minCount, maxCount + 1));
    }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        AnnounceSpawner();
    }

    public void ToggleSpawning()
    {
        spawning = !spawning;
    }

    protected Transform OffsetTransform(Transform transform)
    {
        Vector3 offset = new Vector3(Random.Range(-xOffset, xOffset), 0, Random.Range(-zOffset, zOffset));
        transform.position += offset;
        return transform;
    }
}
