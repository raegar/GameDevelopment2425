using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SettlerSystem;

public class VikingRandomiser : MonoBehaviour
{
    private bool randomisedSuccessfully = false;

    [Header("Settings")]
    [SerializeField] private bool randomiseLooks = true;
    [SerializeField] private bool randomiseGender = true;

    private GameObject vikingPrefab;

    [Header("Looks")]
    [SerializeField] private GameObject[] vikingMeshesMale;
    [SerializeField] private GameObject[] vikingMeshesFemale;
    private GameObject vikingMesh;

    [Header("Gender")]
    [SerializeField] private Gender gender; // if not random, else it's ignored

    [Header("Naming System To Use")]
    [SerializeField] private NameSystemType systemSelection;

    [Header("Script References")]
    [SerializeField] private OnClickSound soundScript;
    [SerializeField] private GrabSettlerFromFactory settlerStatsScript;

    [Header("SFX")]
    [SerializeField] private int femaleGruntIndex = 5;
    [SerializeField] private int maleGruntIndex = 2;

    private void Awake()
    {
        vikingPrefab = gameObject;
    }

    private void Start()
    {
        settlerStatsScript.ChangeNameSystem(systemSelection);
        RandomiseViking();
        settlerStatsScript.GetName();
        DestroyMyself();
    }

    private void RandomiseViking()
    {
        if (randomiseGender)
        {
            RandomiseGender();
        }
        if (randomiseLooks)
        {
            RandomiseLooks();
        }
    }

    private void RandomiseGender()
    {
        gender = Random.Range(0, 2) == 0 ? Gender.Male : Gender.Female;
        settlerStatsScript.gender = gender;

        if (gender == Gender.Male)
        {
            soundScript.ChangeIndex(maleGruntIndex);
        }
        else
        {
            soundScript.ChangeIndex(femaleGruntIndex);
        }
    }

    private void RandomiseLooks()
    {
        if (gender == Gender.Male)
        {
            vikingMesh = vikingMeshesMale[Random.Range(0, vikingMeshesMale.Length)];
            vikingMesh.SetActive(true);

            foreach (GameObject vikingMeshObject in vikingMeshesMale)
            {
                if (vikingMeshObject != vikingMesh)
                {
                    Destroy(vikingMeshObject); // we destroy the other objects to declutter the hierarchy, and to save on performance
                }
            }
        }
        else
        {
            vikingMesh = vikingMeshesFemale[Random.Range(0, vikingMeshesFemale.Length)];
            vikingMesh.SetActive(true);

            foreach (GameObject vikingMeshObject in vikingMeshesMale)
            {
                if (vikingMeshObject != vikingMesh)
                {
                    Destroy(vikingMeshObject);
                }
            }
        }
    }

    private void DestroyMyself()
    {
        randomisedSuccessfully = true;
        Destroy(this);
    }

    private void OnDestroy()
    {
        if (randomisedSuccessfully)
        {
            Debug.Log($"{gameObject.name} has been randomised successfully");
        }
        else
        {
            Debug.LogError($"{gameObject.name} has not been randomised successfully. Check if this script is deleted before it can finish it's operations.", this);
        }
    }
}
