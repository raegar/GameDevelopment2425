using UnityEngine;
using UnityEngine.InputSystem;

public class UnitPCControls : BaseUnitControls
{
    [SerializeField] private InputActionAsset inputActionsAsset;
    [SerializeField] private InputAction moveAction;

    protected override void Awake()
    {
        SetReferences();
    }

    private void Update()
    {
        if (moveAction != null && moveAction.triggered)
        {
            OnActionPerformed(moveAction);
        }
    }

    protected override void SetReferences()
    {
        base.SetReferences();

        if (inputActionsAsset != null)
        {
            moveAction = inputActionsAsset.FindActionMap("Command").FindAction("MoveTo");
            
            if (moveAction == null)
            {
                Debug.LogError("MoveTo action not found in the Command action map.");
            }
        }
        else
        {
            Debug.LogError("Input Actions Asset is not set in the inspector.");
        }
    }

    private void OnActionPerformed(InputAction inputAction)
    {
        // Can be extended for other actions

        if (inputAction == moveAction)
        {
            Debug.Log("Move action pressed.");
            if (unitCommandScript.mouseOver)
            {
                unitCommandScript.SelectUnit();
            }
            else
            {
                //move action (from point A to B) here
            }
        }
    }
}