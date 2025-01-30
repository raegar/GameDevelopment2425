using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class BuildingManager : MonoBehaviour
{
    private ChangeObjectAlpha ChangeObjectAlpha;

    private GameObject selectedStructure;
    private GameObject selectedComponent;
    public GameObject componentToCreate;

    private List<GameObject> buildingPrefabs = new List<GameObject>();
    public GameObject buildingMenu;
    public GameObject buildingButton;
    private bool holdingComponent;

    private Vector3 currentRotation;


    // Start is called before the first frame update
    void Start()
    {
        ChangeObjectAlpha = FindAnyObjectByType<ChangeObjectAlpha>();
        LoadComponentPrefabs();
        CreateComponentMenuButtons();
        holdingComponent = false;
    }

    // Update is called once per frame
    void Update()
    {
        SelectStructure();
        DeleteStructure();

        if (holdingComponent)
        {
            MoveAndSnapComponent();

            if (Input.GetMouseButtonDown(0))
            {

                DropOrPlaceComponent();
                InstantiateComponent();
                componentToCreate.gameObject.transform.eulerAngles = currentRotation;

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopHoldingComponent();
            }

        }
    }

    private void InstantiateComponent()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!RaycastWithoutTriggers(ray, out hit)) { return; }
        componentToCreate = Instantiate(selectedComponent, hit.point, Quaternion.identity);
        SetCollidersEnabled(false);
    }

    private void MoveAndSnapComponent()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!RaycastWithoutTriggers(ray, out hit)) { return; }
        Vector3 snappedPosition = new Vector3(
        Mathf.Round(hit.point.x / 2) * 2,
        Mathf.Round(hit.point.y),
        Mathf.Round(hit.point.z / 2) * 2
        );
        componentToCreate.transform.position = snappedPosition;
        RotateComponent();
        //ChangeObjectAlpha.SetAlpha(componentToCreate, 0.5f);
    }

    private void DropOrPlaceComponent()
    {
        //RaycastForHotbarButton();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!RaycastWithoutTriggers(ray, out hit)) { return; }
        if (hit.collider.CompareTag("Structure"))
        {
            Destroy(componentToCreate);
        }
        else
        {
            //ChangeObjectAlpha.SetAlpha(componentToCreate, 1f);
            componentToCreate.GetComponent<Renderer>().material.color = Color.white;
            SetCollidersEnabled(true);
        }
        
    }


    void LoadComponentPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("BuildingPrefabs");
        foreach (GameObject prefab in prefabs)
        {
            buildingPrefabs.Add(prefab);
        }
    }

    private void CreateComponentMenuButtons()
    {
        foreach (GameObject prefab in buildingPrefabs)
        {
            GameObject button = Instantiate(buildingButton);
            button.transform.SetParent(buildingMenu.gameObject.transform, false);
            button.name = prefab.name;
            SetButtonData(button.name, button);
        }
    }

    public void SetButtonData(string componentName, GameObject button)
    {
        foreach (GameObject prefab in buildingPrefabs)
        {
            if (componentName == prefab.name)
            {
                Image buttonImage = button.GetComponent<Image>();
                buttonImage.sprite = SetButtonImage(componentName);
                buttonImage.type = Image.Type.Simple;
                //HotbarButtons script = button.GetComponent<HotbarButtons>();
                //if (script != null)
                //{
                //    script.componentPrefab = prefab;
                //}
            }
        }
    }

    public Sprite SetButtonImage(string componentName)
    {
        Sprite[] icons = Resources.LoadAll<Sprite>("BuildingIcons");
        foreach (Sprite icon in icons)
        {
            if (icon.name == componentName)
            {
                return icon;
            }
        }
        return null;
    }


    

    public void BuildMenuButtonPressed(string name)
    {
        foreach (GameObject prefab in buildingPrefabs)
        {
            if (name == prefab.name)
            {
                if (holdingComponent) { StopHoldingComponent(); }
                selectedComponent = prefab;
                holdingComponent = true;
                currentRotation = Vector3.zero;
                InstantiateComponent();
            }
        }
    }

    public void StopHoldingComponent()
    {
        Destroy(componentToCreate);
        componentToCreate = null;
        holdingComponent = false;
    }


    private void SetCollidersEnabled(bool enabled)
    {
        Collider[] colliders = componentToCreate.GetComponents<Collider>();
        foreach (Collider collider in colliders) { collider.enabled = enabled; }
    }

    private bool RaycastWithoutTriggers(Ray ray, out RaycastHit hit)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
        foreach (RaycastHit raycastHit in hits)
        {
            if (!raycastHit.collider.isTrigger)
            {
                hit = raycastHit;
                return true;
            }
        }
        hit = new RaycastHit();
        return false;
    }




    private void RotateComponent()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            componentToCreate.gameObject.transform.Rotate(0, 90f, 0);
            currentRotation.y += 90;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            componentToCreate.gameObject.transform.Rotate(0, 90f, 0);
            currentRotation.y -= 90;
        }

    }


    void SelectStructure()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedStructure != null)
            {
                Renderer renderer = selectedStructure.transform.GetComponent<Renderer>();
                renderer.material.color = Color.white;
                selectedStructure = null;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) { return; }
            if (hit.collider.transform.CompareTag("Structure") && !holdingComponent)
            {
                Renderer renderer = hit.transform.GetComponent<Renderer>();
                renderer.material.color = Color.red;
                selectedStructure = hit.transform.gameObject;
            }
            else
            {

                selectedStructure = null;
            }
            
        }

    }

    private void DeleteStructure()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (selectedStructure != null)
            {
                Destroy(selectedStructure);
                selectedStructure = null;
            }
        }
    }

}
