using UnityEngine;
using UnityEngine.UI;


public class AutoScroll : MonoBehaviour
{
    ScrollRect scrollRect;
    public float scrollSpeed = 0.1f;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 4f;

        //disable manual scrolling
        scrollRect.vertical = false;
        scrollRect.horizontal = false;
    }
    private void Update()
    {
        if (scrollRect.verticalNormalizedPosition > -5)
        {
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;
        }
    }
}
