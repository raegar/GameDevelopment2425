using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerCriteriaHandler : MonoBehaviour
{
    public static SpawnerCriteriaHandler Instance { get; private set; }

    public delegate void SpawnAction();
    public static event SpawnAction OnSpawnCriteriaMet;

    [Header("Criteria Settings")]
    [SerializeField] private int blocksToPlace = 8;
    [SerializeField] private int blocksPlaced = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Spawn()
    {
        OnSpawnCriteriaMet?.Invoke();
        blocksPlaced++;
    }

    private void CheckCriteria()
    {
        if (blocksPlaced >= blocksToPlace)
        {
            Spawn();
            blocksPlaced = 0;
        }
    }

    public void AddBlock()
    {
        blocksPlaced++;
        CheckCriteria();
    }

    public void RemoveBlock()
    {
        blocksPlaced--;
        CheckCriteria();
    }
}
