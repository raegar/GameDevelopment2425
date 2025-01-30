using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingTracker : MonoBehaviour
{
    private static bool isQuitting = false;

    private void Awake()
    {
        PopulationManager.Instance.AddMeToPopulation(gameObject);
    }

    private void OnApplicationQuit() // check if application is quitting to not cause OnDestroy errors
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            PopulationManager.Instance.RemoveMeFromPopulation(gameObject);
        }
    }
}
