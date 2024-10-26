using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace GameCamera { 
    public class CameraSystemInput : MonoBehaviour
    {
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    // Movement fields
    [SerializeField] private float moveSpeed = 50f;     // expose as a player preference setting
    [SerializeField] private bool useEdgeScroll = true; // expose as a player preference setting
    [SerializeField] private int edgeScrollSize = 20;
    [SerializeField] private bool useDragPan = true;    // expose as a player preference setting
    [SerializeField] private float dragPanSpeed = 1f;   // expose as a player preference setting
    // Rotation fields
    [SerializeField] private float rotateSpeed = 100f;  // expose as a player preference setting
    //Zoom fields
    [SerializeField] private float zoomMax = 50;
    [SerializeField] private float zoomMin = 10;
    [SerializeField] private float initialZoom = 0;
    [SerializeField] private float zoomSpeed = 10f;     // expose as a player preference setting
    [SerializeField] private float zoomAmount = 5f;

                     private float targetFieldOfView = 25;
                     private bool dragPanMoveActive = false;
                     private Vector2 moveDirection;
                     private Vector2 lastPointerPosition;
                     private float rotateDir = 0f;
                     private Vector2 mouseScrollDelta;

    /// <summary>
    /// MonoBehaviour method called during initialization
    /// </summary>
    private void Awake()
    {
            // check the initial zoom value is not greater than the max zoom value
            if (initialZoom >zoomMax) { initialZoom = zoomMax; }
        // Set the initial field of view to half the max value if it is not set in the inspector
        if (initialZoom == 0) { cinemachineVirtualCamera.m_Lens.FieldOfView = zoomMax/2;}

    }
    /// <summary>
    /// MonoBehaviour method called every frame
    /// </summary>
    private void Update()
    {
        // use boolean flags to ensure that not every method is called every frame
        MoveCameraTarget(moveDirection);
        if (rotateDir != 0f) HandleCameraRotation();
        if (useEdgeScroll) HandleEdgeScrolling();
        if (useDragPan && dragPanMoveActive) HandleCameraDrag();
        if (mouseScrollDelta.y != 0) HandleCameraZoom();
    }

    //----------------- Movement Methods -----------------
    /// <summary>
    /// Called from the PlayerInput component in response to input from the player
    /// </summary>
    /// <param name="context">Data about the player input that occurred</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // read the direction the player wants to move in
        moveDirection = context.ReadValue<Vector2>();
    }
    /// <summary>
    /// Called from the PlayerInput component in response to input from the player
    /// </summary>
    /// <param name="context">Data about the player input that occurred</param>
    public void OnCameraDrag(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            dragPanMoveActive = true;
            lastPointerPosition = Pointer.current.position.ReadValue();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            dragPanMoveActive = false;
        }
    }
    /// <summary>
    /// Moves the camera target when the pointer is at the edge of the screen
    /// </summary>
    private void HandleEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        // Get the current pointer position
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        // Check if the pointer is at the edge of the screen and set the input direction accordingly
        if (pointerPosition.x < edgeScrollSize) { inputDir.x = -1f; }
        if (pointerPosition.y < edgeScrollSize) { inputDir.z = -1f; }
        if (pointerPosition.x > Screen.width - edgeScrollSize) { inputDir.x = +1f; }
        if (pointerPosition.y > Screen.height - edgeScrollSize) { inputDir.z = +1f; }
        // Move the camera target based on the input direction
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// Moves the camera target based on the input direction
    /// </summary>
    /// <param name="direction">the direction to move in</param>
    public void MoveCameraTarget(Vector2 direction)
    {
        Vector3 moveDir = transform.forward * direction.y + transform.right * direction.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// Handles the movement of the camera target from holding the right mouse button and moving the mouse
    /// </summary>
    private void HandleCameraDrag()
    {
        // We need a Vector3 to transform the input into world space
        Vector3 inputDir = new Vector3(0, 0, 0);
        // from the pointers Vector2 data
        Vector2 mouseMovementDelta = Pointer.current.delta.ReadValue();
        inputDir.x = mouseMovementDelta.x * dragPanSpeed;
        inputDir.z = mouseMovementDelta.y * dragPanSpeed;
        // once composed move the camera target
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    //----------------- Rotation Methods -----------------
    /// <summary>
    /// Called from the PlayerInput component in response to input from the player
    /// </summary>
    /// <param name="context">Data about the player input that occurred</param>
    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        // Set the rotation direction based on the input
        rotateDir = +1f;
        // Stop rotating when the input is released
        if (context.phase == InputActionPhase.Canceled) rotateDir = 0f;
    }
    /// <summary>
    /// Called from the PlayerInput component in response to input from the player
    /// </summary>
    /// <param name="context">Data about the player input that occurred</param>
    public void OnRotateRight(InputAction.CallbackContext context)
    {
        // Set the rotation direction based on the input
        rotateDir = -1f;
        // Stop rotating when the input is released
        if (context.phase == InputActionPhase.Canceled) rotateDir = 0f;
    }
    /// <summary>
    /// Rotate the camera based on the input from the player
    /// </summary>    
    private void HandleCameraRotation()
    {
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
    // ----------------- Zoom Methods --------------------
    /// <summary>
    /// Called from the PlayerInput component in response to input from the player
    /// </summary>
    /// <param name="context">Data about the player input that occurred</param>
    public void OnZoom(InputAction.CallbackContext context) 
    {
        mouseScrollDelta = context.ReadValue<Vector2>();
    }
    /// <summary>
    /// Controls the camera zoom based on the mouse scroll wheel input
    /// </summary>
    private void HandleCameraZoom()
    {
        // Checks if wea are zooming in or out and adjusts the target field of view accordingly
        if (mouseScrollDelta.y > 0) {targetFieldOfView -= zoomAmount;}
        if (mouseScrollDelta.y < 0) {targetFieldOfView += zoomAmount;}
        // Clamps the target field of view to the min and max values
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, zoomMin, zoomMax);
        // Smoothly lerps the camera's field of view to the target field of view
        cinemachineVirtualCamera.m_Lens.FieldOfView =
        Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    // ----------------- Stretch goal Handle touch pinch to zoom --------------------
    }
}