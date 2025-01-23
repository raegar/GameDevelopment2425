using UnityEngine;

[RequireComponent(typeof(UnitMovementScript))]
public class UnitCommandScript : MonoBehaviour
{
    [Header("Main References")]
    public bool mouseOver = false;
    [SerializeField] private UnitMovementScript unitMovementScript;

    private void Awake()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        unitMovementScript = GetComponent<UnitMovementScript>();
    }

    public void SelectUnit()
    {
        UnitControlManager.Instance.SelectUnit(unitMovementScript);
    }

    public void MoveUnit(Vector3 destination)
    {
        UnitControlManager.Instance.Order(destination);
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}