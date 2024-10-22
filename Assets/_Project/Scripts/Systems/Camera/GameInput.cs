
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour , IPointerMoveHandler,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private bool useEdgeScroll = true;
    private Vector2 moveDirection;
    private Vector2 lastPointerPosition;

    private void Update()
    {
        MoveCameraTarget(moveDirection);
        if (useEdgeScroll) HandleCameraMovementEdgeScrolling();
        
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    
    public void MoveCameraTarget(Vector2 direction)
    {
        Vector3 moveDir = transform.forward * direction.y + transform.right * direction.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        Vector2 pointerPosition = Pointer.current.position.ReadValue();

        int edgeScrollSize = 20;

        if (pointerPosition.x < edgeScrollSize) {inputDir.x = -1f;}
        if (pointerPosition.y < edgeScrollSize) {inputDir.z = -1f;}
        if (pointerPosition.x > Screen.width - edgeScrollSize) {inputDir.x = +1f;}
        if (pointerPosition.y > Screen.height - edgeScrollSize) {inputDir.z = +1f;}
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }


    public void OnPointerMove(PointerEventData eventData)
    {
        lastPointerPosition = eventData.position;
        Debug.Log("Pointer Move" + lastPointerPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       Debug.Log("Pointer Up");
    }
}
