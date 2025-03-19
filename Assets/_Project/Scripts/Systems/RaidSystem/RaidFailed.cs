using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidFailed : PanelBase
{
    public void OpenPanel()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void ClosePanel()
    {
        UIManager.Instance.ClosePanel(this);
    }
}
