using PatternLibrary;
using UnityEngine;

public class DemoUIManager : Singleton<DemoUIManager>
{
    public GameObject[] canvasObjects;

    public void ToggleCanvas(int index)
    {
        switch (canvasObjects[index].activeSelf)
        {
            case true:
                canvasObjects[index].SetActive(false);
                break;
            case false:
                canvasObjects[index].SetActive(true);
                break;
        }
    }
}
