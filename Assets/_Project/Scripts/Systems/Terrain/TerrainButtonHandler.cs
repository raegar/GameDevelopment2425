using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainButtonHandler : MonoBehaviour, IPointerDownHandler
{
    TerrainUI terrainUI;
    public void Start()
    {
        terrainUI = FindObjectOfType<TerrainUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        terrainUI.RaiseTerrain();
        Debug.Log("here");
    }

   
}
