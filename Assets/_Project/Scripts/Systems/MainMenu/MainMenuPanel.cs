using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : PanelBase
{
    private void Awake()
    {
        base.Awake();
        OpenMainMenuPanel();
    }
    public void OpenMainMenuPanel()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void CloseMainMenuPanel()
    {
        UIManager.Instance.ClosePanel(this);
    }
}
