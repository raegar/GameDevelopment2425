using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerCriteriaHandler))]
public class VikingEventSpawner : BaseVikingSpawner
{
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
        SpawnerCriteriaHandler.OnSpawnCriteriaMet += Spawn;
    }

    private void OnDisable()
    {
        SpawnerCriteriaHandler.OnSpawnCriteriaMet -= Spawn;
    }
}
