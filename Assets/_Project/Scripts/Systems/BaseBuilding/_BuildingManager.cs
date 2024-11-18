using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class _BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private Material[] materials;
    public GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;

    public float rotateAmount;
    public float gridSize;
    bool gridOn = true;

    public bool canPlace = true;

    [SerializeField] private Toggle gridToggle;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                    );
            }
            else
            {
                pendingObject.transform.position = pos;
            }

            UpdateMaterials();

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            
        }
    }

    public void PlaceObject() //Method to place objects once they have been selected
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        pendingObject = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index) //Allows for selection of the object from the UI buttons provided
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }

    public void ToggleGrid() //Enables the UI toggle to enable grid placement or to disable it
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    public void RotateObject() //Simple rotation method to allow objects to be rotated with a specified amount in the inspector
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    void UpdateMaterials() //method to update materials of the object when the trigger says that the object can either be placed or cannot
    {
        if(canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        if (!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }
}