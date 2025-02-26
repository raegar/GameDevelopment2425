using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingEventSpawner : BaseVikingSpawner
{
    public delegate void SpawnAction();
    public static event SpawnAction OnSpawnCriteriaMet;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (initialVikingCount > 0)
        {
            Spawn(initialSpawn, initialVikingCount);
        }
    }

    protected override void AnnounceSpawner()
    {
        Debug.Log($"VikingEventSpawner: Using event viking spawner", this);
    }

    private void OnEnable()
    {
        OnSpawnCriteriaMet += Spawn;
    }

    private void OnDisable()
    {
        OnSpawnCriteriaMet -= Spawn;
    }
}
