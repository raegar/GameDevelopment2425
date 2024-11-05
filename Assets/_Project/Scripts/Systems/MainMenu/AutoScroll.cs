/*Author: Jess Woodward
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: this scipt makes it so the credits autoscroll through till the end of the text
 */
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    ScrollRect scrollRect;
    public float scrollSpeed = 0.1f;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
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
    public void ResetCredits()
    {
        scrollRect.verticalNormalizedPosition = 4f;
    }
}
