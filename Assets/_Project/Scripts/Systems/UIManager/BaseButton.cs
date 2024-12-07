using GameProjectManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    protected Button button;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }
    protected virtual void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }
    protected virtual void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }
    protected virtual void OnClick()
    {

    }
}
