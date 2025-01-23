using PatternLibrary;
using UnityEngine;

public class UnitControlManager : Singleton<UnitControlManager> 
{
    [Header("References")]
    public UnitMovementScript selectedUnit;
    public Camera gameCamera;
    public GameObject settlerPanel, inventoryPanel;
    public Material lineMaterial;

    private void Update()
    {
        if (selectedUnit != null)
        {
            if (Input.GetMouseButtonDown(0) && !selectedUnit.mouseOver)
            {
                DeselectUnit(selectedUnit);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint))
                {
                    Vector3 finalPos = new Vector3(hitPoint.point.x, hitPoint.point.y, hitPoint.point.z);
                    Order(finalPos);
                    Debug.Log($"Mouse pos: {ray} Hitpoint pos: {hitPoint.point} Final pos: {finalPos}");
                }
            }
        }
    }

    private void SetReferences()
    {
        if (inputActionsAsset != null)
        {
            moveAction = inputActionsAsset.FindActionMap("Command/MoveTo").FindAction("MoveTo");

            moveAction.performed += ctx => SelectUnit();
        }
        else
        {
            Debug.LogError("Input Actions Asset is not set in the inspector.");
        }
    }


    public void SelectUnit(UnitMovementScript unit)
    {
        if (selectedUnit != null)
        {
            DeselectUnit(selectedUnit);
        }
        selectedUnit = unit;
        SelectionEffects selectionEffects = selectedUnit.GetComponent<SelectionEffects>();
        if (selectionEffects != null)
        {
            selectionEffects.selectedIcon.SetActive(true);
        }
        UIManager.Instance.OpenPanel(settlerPanel.GetComponent<PanelBase>());
        if (unit.affiliation == Affiliation.Friendly)
        {
            UIManager.Instance.OpenPanel(inventoryPanel.GetComponent<PanelBase>());
        }
    }

    public void DeselectUnit(UnitMovementScript unit)
    {
        SelectionEffects selectionEffects = selectedUnit.GetComponent<SelectionEffects>();
        if (selectionEffects != null)
        {
            selectionEffects.selectedIcon.SetActive(false);
        }
        selectedUnit = null;
        UIManager.Instance.ClosePanel(settlerPanel.GetComponent<PanelBase>());
        UIManager.Instance.ClosePanel(inventoryPanel.GetComponent<PanelBase>());
    }
    public void Order(Vector3 orderPos)
    {
        if (selectedUnit.affiliation != Affiliation.Friendly)
        {
            return;
        }
        selectedUnit.agent.isStopped = true;
        selectedUnit.agent.destination = orderPos;
        SelectionEffects selectionEffects = selectedUnit.GetComponent<SelectionEffects>();
        
        if (selectionEffects != null)
        {
            selectionEffects.destinationIcon.transform.position = new Vector3(orderPos.x, orderPos.y + 0.1f, orderPos.z);
            selectionEffects.destinationIcon.transform.parent = this.transform;
            selectionEffects.destinationIcon.SetActive(true);
        }

        selectedUnit.agent.isStopped = false;
        Debug.Log($"{gameObject.name} {selectedUnit.agent.destination}");
    }
}
