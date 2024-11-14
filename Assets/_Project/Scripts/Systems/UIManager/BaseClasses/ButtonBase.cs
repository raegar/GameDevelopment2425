using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{ // This class is used to handle the button events

    Button button;
    Image buttonImage;
    TMP_Text buttonText;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //noop
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //noop
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //noop
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //noop
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //noop
    }
}
