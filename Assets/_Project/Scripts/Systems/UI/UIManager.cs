using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatternLibrary;
using System;
using AYellowpaper.SerializedCollections;

public class UIManager : Singleton<UIManager>
{
    public PanelListSO listOfPanels;
    //              Panel // isVisible
    public SerializedDictionary<Panel, bool> panels = new SerializedDictionary<Panel, bool>();
    internal void Register(Panel panel)
    {

        panels.TryAdd(panel, false);
        //must be invisable

    }

    // Open
    public void OpenPanel(Panel panel)
    {
        panel.gameObject.SetActive(true);
        panels.TryGetValue(panel, out bool found);
        if (found)
        {
            //// change to true
        }
        else
        {
            panels.Add(panel, false);
        }

    }
    //close
    public void ClosePanel(Panel panel)
    {
        panel.gameObject.SetActive(false);
        panels.TryGetValue(panel, out bool found);
        if (found)
        {
            //// change to true
        }
        else
        {
            panels.Add(panel, false);
        }
    }


    // close all

    public void CloseAllPanels(Panel panel)
    {
        panel.gameObject.SetActive(false);

        foreach(KeyValuePair<Panel, bool> mypanel in panels) 
        {
            //mypanel.Value = false;
        }

    }
}
