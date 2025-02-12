using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingTracker : MonoBehaviour
{
    private static bool isQuitting = false;
    private int ID = -1;

    private void Awake()
    {
        PopulationManager.Instance.AddMeToPopulation(this);
    }

    private void OnApplicationQuit() // check if application is quitting to not cause OnDestroy errors
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            PopulationManager.Instance.RemoveMeFromPopulation(ID);
        }
    }

    public void SetID(int id)
    {
        ID = id;
    }

    public int GetID()
    {
        return ID;
    }
}
