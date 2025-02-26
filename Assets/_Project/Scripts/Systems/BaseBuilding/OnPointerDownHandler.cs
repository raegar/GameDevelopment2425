using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerDownHandler : MonoBehaviour, IPointerUpHandler
{

    BuildingManager buildingManager;

    public void OnPointerUp(PointerEventData eventData)
    {
        buildingManager.BuildMenuButtonPressed(name);
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindAnyObjectByType<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
