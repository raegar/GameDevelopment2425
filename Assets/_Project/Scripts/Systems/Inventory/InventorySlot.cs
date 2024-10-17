using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                DragAndDrop inventoryItem = eventData.pointerDrag.GetComponent<DragAndDrop>();
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }
}
