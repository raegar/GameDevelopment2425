using PatternLibrary;
using UnityEngine;
using static UnitMovementScript;

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
                    if (hitPoint.collider.tag == "Interactable") 
                    {
                        // do interactor stuff
                    }
                    else
                    {
                        Vector3 finalPos = new Vector3(hitPoint.point.x, hitPoint.point.y, hitPoint.point.z);
                        Order(finalPos);
                        Debug.Log($"Mouse pos: {ray} Hitpoint pos: {hitPoint.point} Final pos: {finalPos}");
                    }
                }
            }
        }
    }

    public void SelectUnit(UnitMovementScript unit)
    {
        if (selectedUnit != null)
        {
            DeselectUnit(selectedUnit);
        }
        selectedUnit = unit;
        selectedUnit.selectedIcon.SetActive(true);
        UIManager.Instance.OpenPanel(settlerPanel.GetComponent<PanelBase>());
        if (unit.affiliation == Affiliation.Friendly)
        {
            UIManager.Instance.OpenPanel(inventoryPanel.GetComponent<PanelBase>());
        }
    }

    public void DeselectUnit(UnitMovementScript unit)
    {
        selectedUnit.selectedIcon.SetActive(false);
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
        selectedUnit.destinationIcon.transform.position = new Vector3(orderPos.x, orderPos.y + 0.1f, orderPos.z);
        selectedUnit.destinationIcon.transform.parent = this.transform;
        selectedUnit.destinationIcon.SetActive(true);
        selectedUnit.agent.isStopped = false;
        Debug.Log($"{gameObject.name} {selectedUnit.agent.destination}");
    }
}
