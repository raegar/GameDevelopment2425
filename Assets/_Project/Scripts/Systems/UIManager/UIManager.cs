/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :This script is designed as single point of access for all UI panels in the game.           
 */

using PatternLibrary;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // reference to the registered panels scriptable object contining all the panels in the game
    public RegisteredPanelsSO  NamedPanelPrefabs;
    // reference to the canvas root
    public GameObject canvasRoot;
    // a dictionary of current panels and their visibility
    [Header("For debug purposes only - please do not manually add")]
    [SerializeField] private SerializedDictionary<string, PanelBase> panelsToInstantiate; 
    [SerializeField] private SerializedDictionary<string, PanelBase> registeredPanels;
    
    protected override void Awake()
    {
        // enable singleton functionality
        base.Awake();
        // move the panels from the scriptable object to the dictionary
        panelsToInstantiate = NamedPanelPrefabs.panels;
     
        InitializePanels();
    }

    /// <summary>
    /// Instantiates all the prefabs in the panelsToInstantiate dictionary
    /// into gameObjects and stores the references in the registeredPanels dictionary
    /// </summary>
    private void InitializePanels()
    {
        foreach (var panel in panelsToInstantiate)
        {
            var _go = Instantiate(panel.Value,canvasRoot.transform);
            _go.name = panel.Key;
            Debug.Log("Instantiated panel: " + panel.Key);
            // if the panel is already registered, log a warning
            if (!registeredPanels.ContainsKey(panel.Key))
            {
                registeredPanels.Add(panel.Key, _go);
                Debug.Log("Registered panel: " + _go);
                ClosePanel(panel.Key);
            }
        }
    }

    /// <summary>
    /// Opens a named panel
    /// </summary>
    /// <param name="panel">panel to open</param>
    public void OpenPanel(string panel)
    { 
     if (registeredPanels.ContainsKey(panel))
        { registeredPanels[panel].gameObject.SetActive(true);}
        else
        { Debug.LogWarning("Panel not found in dictionary");}
    }

    /// <summary>
    /// closes a named panel
    /// </summary>
    /// <param name="panel">panel to close</param>
    public void ClosePanel(string panel)
    {
        if (registeredPanels.ContainsKey(panel))
        { registeredPanels[panel].gameObject.SetActive(false);}
        else
        { Debug.LogWarning("Panel not found in dictionary"); }
    }

    // A OpenAllPanels() method has NOT been included as it is not a expected use case

    /// <summary>
    /// Closes all registered panels
    /// </summary>
    public void CloseAllPanels()
    {
        // Move the keys to a list to avoid the .Net 2.1 error
        foreach (var panel in registeredPanels)
        {
            panel.Value.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Returns the bool status of a panel
    /// </summary>
    /// <param name="panel">panel to check</param>
    /// <returns>actve/inactive</returns>
    public bool PanelStatus(string panel)
    {
        if (registeredPanels.ContainsKey(panel))
        { return registeredPanels[panel].gameObject.activeInHierarchy;}
        else
        {
            Debug.LogWarning("Panel not found in dictionary");
            return false;
        }
    }
}
