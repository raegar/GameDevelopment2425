using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(PlayerInput))]
public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction pointerPositionAction;



    /// <summary>
    /// Cache the PlayerInput and the InputActions when the component is created to improve performance.
    /// </summary>
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if(playerInput.notificationBehavior != PlayerNotifications.InvokeCSharpEvents)
        {
            playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        }
    }

    /// <summary>
    /// Subscribe to the PlayerInput actions when the component is enabled
    /// </summary>
    private void OnEnable()
    {
        if (playerInput != null)
        {
            moveAction.started  += ctx => Move(ctx.ReadValue<Vector2>(), false);
            moveAction.canceled += ctx => Move(ctx.ReadValue<Vector2>(), true);
            playerInput.actions["Pointer Position"].performed += ctx => PointerPosition(ctx.ReadValue<Vector2>());
            playerInput.actions["Rotate Left"].performed += ctx => RotateLeft(ctx);

        }
    }

    private void RotateLeft(InputAction.CallbackContext ctx)
    {
        Debug.Log("Rotate Left");
    }

    /// <summary>
    /// Unsubscribe from the PlayerInput actions when the component is disabled 
    /// this should not happen but is important to avoid memory leaks
    /// </summary>
    private void OnDisable()
    {
        
    }

    // no monobehaviour update methods are used to stay off the hot-path as much as possible.

    /// <summary>
    /// Called by the PlayerInput system when the Move action is performed.
    /// </summary>
    /// <param name="vector2">the forward/back/left/right vector of the move</param>
    public void Move(Vector2 vector2, bool canceled)
    {
        Debug.Log("Move: " + vector2);  
        while (!canceled)
        {
            CameraManager.Instance.MoveCamera(vector2);
        }
    }

    /// <summary>
    /// Called by the PlayerInput system only whenever the Pointer Position changes to avoid constant polling
    /// </summary>
    /// <param name="vector2"></param>
    public void PointerPosition(Vector2 vector2)
    {
        Debug.Log("Pointer Position: " + vector2);
    }
}
