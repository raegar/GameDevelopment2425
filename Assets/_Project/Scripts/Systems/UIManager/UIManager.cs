/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 25/10/2024
 * Purpose      :This script is designed as single point of access for all UI panels in the game.
 *               
 */
using System.Collections.Generic;
using UnityEngine;
using PatternLibrary;
using AYellowpaper.SerializedCollections;

public class UIManager : Singleton<UIManager>
{
    public GameObject UICanvasPrefab;
    // a list of panel child classes that should be in the game
    public PanelListSO panelsToInstantiate;
    // a data container where designers can list the panels that should be in the game 
    public GameObject  panelContainer;
    // a dictionary of current panels and their visibility
    public SerializedDictionary<PanelBase, bool> panels = new SerializedDictionary<PanelBase, bool>();

    /// <summary>
    /// Unity's Awake method used for initialisation
    /// </summary>
   
    /// <summary>
    /// Registers a panel in the panels dictionary whenever a child class of Panel awakes
    /// </summary>
    /// <param name="panel"></param>
    public void Register(PanelBase panel)
    {
        Debug.Log("Registering panel");
        // if the panel is already registered, log a warning
        if (!panels.TryGetValue(panel, out bool exists))
        {
            panels.Add(panel, false);
            ClosePanel(panel);
        }
        else
        {
            // not critical but shouldn't be happening
            Debug.LogWarning($"{panel} already registered");
        }
    }
    
    /// <summary>
    /// Opens a single panel by enabling the game object attached to the panel component
    /// </summary>
    /// <param name="panel"></param>
    public void OpenPanel(PanelBase panel)
    {
        panels.TryGetValue(panel, out bool found);
        if (found)
        {
           Debug.Log("Opening panel" + panel.name);
            panels[panel] = true;
            panel.gameObject.SetActive(true);
            // can be extended here to add a standardized sound effect or animation
        }
        else {Debug.LogWarning($"{panel} not found in the scene or in the list of panels");
        }
    }

    /// <summary>
    /// Closes a single panel
    /// </summary>
    /// <param name="panel">the panel to be closed</param>
    public void ClosePanel(PanelBase panel)
    {
        panels.TryGetValue(panel, out bool found);
        if (found)
        {
            Debug.Log("Closing panel" + panel.name);
            panels[panel] = false;
            panel.gameObject.SetActive(false);
            // can be extended here to add a standardized sound effect or animation
        }
        else
        {
            Debug.LogWarning($"{panel} not found in the scene or in the list of panels");
        }
    }

    /// <summary>
    /// Closes all registered panels
    /// </summary>
    public void CloseAllPanels()
    {
        foreach(KeyValuePair<PanelBase, bool> mypanel in panels) 
        {
            panels[mypanel.Key] = false;
            mypanel.Key.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Queries the dictionary to see if a panel is open
    /// </summary>
    /// <param name="panel">the panel to check</param>
    /// <returns>true if open / false if closed or not present</returns>
    public bool IsPanelOpen(PanelBase panel)
    {
        panels.TryGetValue(panel, out bool found);
        if (found) {return panels[panel];}
        else
        {
            Debug.LogWarning($"{panel} not found in the scene or in the list of panels");
            return false;
        }
    }


}
