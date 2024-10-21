
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction pointerPositionAction;
    private IEnumerator activeCoroutine;








    /// <summary>
    /// Cache the PlayerInput and the InputActions when the component is created to improve performance.
    /// </summary>
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput = null)
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }
        
    }

    /// <summary>
    /// Subscribe to the PlayerInput actions when the component is enabled
    /// </summary>
    private void OnEnable()
    {
        if (playerInput != null)
        {
            moveAction.started  += ctx => Move(ctx.ReadValue<Vector2>());
            moveAction.canceled += _ => CancelActiveCoroutine();
            playerInput.actions["Pointer Position"].performed += ctx => PointerPosition(ctx.ReadValue<Vector2>());
            playerInput.actions["Rotate Left"].performed += _ => RotateLeft();

        }
    }


    /// <summary>
    /// As this is a critical system this should never be disabled unless the game is exiting, 
    /// but it is good practice to unsubscribe from events when the component is disabled to avoid memory leaks.
    /// </summary>
    private void OnDisable()
    {

    }

    private void RotateLeft()
    {
        Debug.Log("Rotate Left");
    }

    private void RotateRight(InputAction.CallbackContext ctx)
    {
        Debug.Log("Rotate Right");
    }


    /// <summary>
    /// Called by the PlayerInput system when the Move action is performed.
    /// </summary>
    /// <param name="vector2">the forward/back/left/right vector of the move</param>
    public void Move(Vector2 vector2)
    {
        // if another coroutine (such as rotation) is active, cancel it
        StopCoroutine(activeCoroutine);
        // set the coroutine to movement
        activeCoroutine = IMove(vector2);
        // start the coroutine so the action happens continiously until the action is canceled
        StartCoroutine(activeCoroutine);
    }
    /// <summary>
    /// Moves the player in the direction of the vector2 until the action is canceled
    /// </summary>
    /// <param name="direction">Direction to move</param>
    /// <returns></returns>
    private IEnumerator IMove(Vector2 direction)
    {

        yield return null;
    }

    private void CancelActiveCoroutine()
    {
        StopCoroutine(activeCoroutine);
        activeCoroutine = null;
    }

    private void zoomIn() { }
    private void zoomOut() { }

    /// <summary>
    /// Called by the PlayerInput system only whenever the Pointer Position changes to avoid constant polling
    /// </summary>
    /// <param name="vector2"></param>
    public void PointerPosition(Vector2 vector2)
    {
        Debug.Log("Pointer Position: " + vector2);
    }
}
