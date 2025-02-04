using System.Collections;
using UnityEngine;

public class VikingIntervalSpawner : MonoBehaviour
{
    [Header("Viking Settings")]
    [SerializeField] private GameObject vikingPrefab;
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 3;

    [Header("Interval Settings")]
    private bool spawning = true;
    public bool randomInterval = true;
    [SerializeField] private int interval = 300; // seconds
    [SerializeField] private int minInterval = 180; // seconds
    [SerializeField] private int maxInterval = 360; // seconds

    [Header("Location Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform initialSpawn;
    [SerializeField] private float xOffset = 1;
    [SerializeField] private float zOffset = 1;

    [Header("Misc Settings")]
    [SerializeField] private int initialVikingCount = 3;
    [SerializeField] private float minimumVikingSpace = 0.5f;
    private Coroutine spawnVikingCoroutine;

    private void Start()
    {
        if (initialVikingCount > 0)
        {
            Spawn(initialSpawn, initialVikingCount);
        }
    }

    private void Update()
    {
        if (!spawning)
        {
            StopCoroutine(spawnVikingCoroutine);
        }
        else
        {
            if (spawnVikingCoroutine == null)
            {
                spawnVikingCoroutine = StartCoroutine(SpawnViking());
            }
        }
    }

    private IEnumerator SpawnViking()
    {
        yield return new WaitForSeconds(randomInterval ? Random.Range(minInterval, maxInterval) : interval);
        Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)], Random.Range(minCount, maxCount + 1));
        spawnVikingCoroutine = StartCoroutine(SpawnViking());
    }
    private void Spawn(Transform transform, int count)
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
            }
        }
    }

    private Transform OffsetTransform(Transform transform)
    {
        Vector3 offset = new Vector3(Random.Range(-xOffset, xOffset), 0, Random.Range(-zOffset, zOffset));
        transform.position += offset;
        return transform;
    }

    public void ToggleSpawning()
    {
        spawning = !spawning;
    }
}
