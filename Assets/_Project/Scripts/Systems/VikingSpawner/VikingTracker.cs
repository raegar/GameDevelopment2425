using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingTracker : MonoBehaviour
{
    private void Awake()
    {
        PopulationManager.Instance.AddMeToPopulation(gameObject);
    }

    private void OnDestroy()
    {
        if (Application.isFocused) // <- this stops OnDestroy errors from happening when leaving playmode
        {
            if (PopulationManager.Instance != null)
            {
                PopulationManager.Instance.RemoveMeFromPopulation(gameObject);
            }
        }
    }
}
