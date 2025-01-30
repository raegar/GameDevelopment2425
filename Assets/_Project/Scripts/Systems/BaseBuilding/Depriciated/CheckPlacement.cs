using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    _BuildingManager _buildingManager;

    void Start()
    {
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<_BuildingManager>();
    }

    private void OnTriggerStay(Collider other) //Disables placement of objects inside of another object
    {
        if(other.gameObject.CompareTag("Object"))
        {
            _buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other) //Once the object being placed is outside of the trigger area of another object, it can be placed
    {
        if (other.gameObject.CompareTag("Object"))
        {
            _buildingManager.canPlace = true;
        }
    }
}
