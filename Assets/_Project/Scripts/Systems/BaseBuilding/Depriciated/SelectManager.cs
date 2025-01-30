using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public GameObject selectedObject;
    public TextMeshProUGUI objNameText;
    private _BuildingManager _buildingManager;
    public GameObject objUi;

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<_BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Fires a raycast from the mouse position to select the object.
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj) // Selects the game object and sets the objUI to active. NEEDS some kind of outline or change of material to show its selected.
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        objNameText.text = obj.name;
        objUi.SetActive(true);
        selectedObject = obj;
    }

    private void Deselect() // Deselects the object and sets objUI to inactive.
    {
        objUi.SetActive(false);
        selectedObject = null;
    }

    public void Move() // Allows the object to be moved when pressing the UI button.
    {
        _buildingManager.pendingObject = selectedObject;
    }

    public void Delete() // Destroys the object in the scene upon pressing the delete button.
    {
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
    }
}
