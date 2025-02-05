using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerDownHandler : MonoBehaviour, IPointerDownHandler
{

    BuildingManager buildingManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        buildingManager.BuildMenuButtonPressed(name);
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindAnyObjectByType<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
