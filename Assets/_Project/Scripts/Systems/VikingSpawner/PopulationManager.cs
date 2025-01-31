using PatternLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : Singleton<PopulationManager>
{
    [Header("Population Settings")]
    public int startingPopulation = 3;
    public int currentMaxPopulation = 10;
    [SerializeField] [Tooltip("Unchangeable (for the player) population limit")] private int trueMaxPopulation = 20;

    [Header("Population Tracking")]
    public List<GameObject> population = new List<GameObject>();
    public int populationCount = 0;

    public void AddMeToPopulation(GameObject vikingToAdd)
    {
        if (populationCount >= currentMaxPopulation)
        {
            Debug.Log("Population limit reached");
            Destroy(vikingToAdd);
        }
        else
        {
            population.Add(vikingToAdd);
            populationCount++;
        }
    }

    public void RemoveMeFromPopulation(GameObject vikingToRemove)
    {
        population.Remove(vikingToRemove);
        populationCount--;
    }

    public void ChangePopulationCap(int amount)
    {
        currentMaxPopulation += amount;
        currentMaxPopulation = Mathf.Clamp(currentMaxPopulation, 1, trueMaxPopulation);
    }
}
