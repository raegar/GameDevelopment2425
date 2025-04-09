using System.Collections;
using UnityEngine;

public class VikingIntervalSpawner : BaseVikingSpawner
{
    [Header("Interval Settings")]
    public bool randomInterval = true;
    [SerializeField] private int interval = 300; // seconds
    [SerializeField] private int minInterval = 180; // seconds
    [SerializeField] private int maxInterval = 360; // seconds
    private Coroutine spawnVikingCoroutine;

    private void Start()
    {
        if (initialVikingCount > 0)
        {
            Spawn(initialSpawn, initialVikingCount);
        }
    }

    protected override void AnnounceSpawner()
    {
        Debug.Log($"VikingIntervalSpawner: Using interval viking spawner", this);
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
}
