using GameProjectManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSaves : MonoBehaviour
{
    private List<string> saveNames;
    [SerializeField] private GameObject buttonPrefab;
    private int previousCount = 0;

    private void Awake()
    {
        List(); 
    }

    private void List()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        saveNames = GameManager.Instance.ListSaves();
        foreach (string saveName in saveNames)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponentInChildren<LoadButton>().SetLoadName(saveName);
            button.GetComponentInChildren<DeleteButton>().SetDeleteName(saveName);
        }
        previousCount = saveNames.Count;
    }

    private void Update ()
    {
       if (GameManager.Instance.ListSaves().Count != previousCount)
        {
            List();
        }
    }
}
