using PatternLibrary;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : Singleton<PopulationManager>
{
    [Header("Population Settings")]
    public int startingPopulation = 3;
    public int currentMaxPopulation = 10;
    [SerializeField] [Tooltip("Unchangeable (for the player) population limit")] private int trueMaxPopulation = 20;

    [Header("Population Tracking")]
    public Dictionary<int, VikingTracker> population = new Dictionary<int, VikingTracker>();
    public int populationCount = 0;

    public void AddMeToPopulation(VikingTracker vikingToAdd)
    {
        if (populationCount >= currentMaxPopulation)
        {
            Debug.Log("Population limit reached");
            Destroy(vikingToAdd);
        }
        else
        {
            
            if (vikingToAdd.GetID() == -1)
            {
                int ID = GenerateUniqueID();
                vikingToAdd.SetID(ID);
            }

            population.Add(vikingToAdd.GetID(), vikingToAdd);
            populationCount++;
        }
    }

    public void RemoveMeFromPopulation(int ID)
    {
        population.Remove(ID);
        populationCount--;
    }

    public void ChangePopulationCap(int amount)
    {
        currentMaxPopulation += amount;
        currentMaxPopulation = Mathf.Clamp(currentMaxPopulation, 1, trueMaxPopulation);
    }

    private int GenerateUniqueID()
    {
        int id = Random.Range(0, 10000);

        while (population.ContainsKey(id))
        {
            id = Random.Range(0, 10000);
        }

        return id;
    }

    public VikingTracker FindVikingByID(int ID)
    {
        return population[ID];
    }

    public GameObject[] ReturnAllVikings()
    {
        GameObject[] vikingObjects = new GameObject[population.Count];
        VikingTracker[] temp = population.Values.ToArray();

        for (int i = 0; i < population.Count; i++)
        {
            vikingObjects[i] = temp[i].gameObject;
        }

        return vikingObjects;
    }
}