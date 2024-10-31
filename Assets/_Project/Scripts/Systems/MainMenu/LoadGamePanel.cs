using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGamePanel : PanelBase
{
    public void OpenLoadgamePanel()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void CloseLoadgamePanel()
    { 
        UIManager.Instance.ClosePanel(this);
    }
}
