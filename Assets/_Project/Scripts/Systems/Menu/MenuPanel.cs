using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : PanelBase
{
    void Awake()
    {
        base.Awake();
    }
    public void OpenMenu()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void closeMenu()
    {
        UIManager.Instance.ClosePanel(this);
    }
}
