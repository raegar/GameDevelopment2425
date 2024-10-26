/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :This script is designed as single point of access for all UI panels in the game.
 *               
 */
using UnityEngine;
using PatternLibrary;
using AYellowpaper.SerializedCollections;
using System.Linq;


public class UIManager : Singleton<UIManager>
{
    // a dictionary of current panels and their visibility
    [Header("For debug purposes only - please do not manually add")]
    [SerializeField] private SerializedDictionary<PanelBase, bool> panels = new SerializedDictionary<PanelBase, bool>();

    /// <summary>
    /// Registers a panel in the panels dictionary whenever a child class of BasePanel awakes
    /// </summary>
    /// <param name="panel"></param>
    public void Register(PanelBase panel)
    {
        Debug.Log("Registering panel" + panel);
        // if the panel is already registered, log a warning
        if (!panels.ContainsKey(panel))
        {
            panels.Add(panel, false);
            ClosePanel(panel);
        }
        else
        {
            // shouldn't ever happen, but sanity check
            Debug.LogWarning($"{panel} already registered");
        }
    }

    /// <summary>
    /// Opens a single panel by enabling the game object attached to the panel component
    /// </summary>
    /// <param name="panel"></param>
    public void OpenPanel(PanelBase panel)
    {
        // check if the panel is in the dictionary
        if (panels.ContainsKey(panel))
        {
            // change the panels status in the dictionary to disabled (false)
            panels[panel] = true;
            // enable the game object
            panel.gameObject.SetActive(true);
            // can be extended here to add a standardized sound effect or animation
        }
        else
        {
            // this should not happen but if there is a rogue panel in the scene register it
            Register(panel);
            // try again
            OpenPanel(panel);
        }
    }

    /// <summary>
    /// Closes a single panel
    /// </summary>
    /// <param name="panel">the panel to be closed</param>
    public void ClosePanel(PanelBase panel)
    {
        // check if the panel is in the dictionary
        if (panels.ContainsKey(panel))
        {
            // change the panels status in the dictionary to disabled (false)
            panels[panel] = false;
            // disable the game object
            panel.gameObject.SetActive(false);
            // can be extended here to add a standardized sound effect or animation
        }
        else
        {
            // this should not happen but if there is a rogue panel in the scene register it
            Register(panel);
        }
    }

    // A OpenAllPanels() method has not been included as it is not a expected use case

    /// <summary>
    /// Closes all registered panels. Had to use Linq as 
    /// .Net 2.1 does not support changing values during enumeration
    /// </summary>
    public void CloseAllPanels()
    {
        // Move the keys to a list to avoid the .Net 2.1 error
        foreach (var panel in panels.ToList())
        {
            // change the panels status in the dictionary to disabled (false)
            panels[panel.Key] = false;
            // disable the game object
            panel.Key.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Queries the dictionary to see if a panel is open
    /// </summary>
    /// <param name="panel">the panel to check</param>
    /// <returns>true if open / false if closed or not present</returns>
    public bool IsPanelOpen(PanelBase panel)
    {
        if (panels.ContainsKey(panel)) { return panels[panel]; }
        else
        {
            // this should not happen but if there is a rogue panel in the scene register it
            Register(panel);
            return false;
        }
    }
}
