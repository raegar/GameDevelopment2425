/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :A simple script to demonstrate inheritance from the PanelBase.cs script
 */
using UnityEngine;

public class InventoryPanel : PanelBase
{
    protected override void Awake()
    {
        // Dont remove this. This is required to register the panel with the UIManager
        base.Awake();
        // do anything else that you want to do when your script wakes up
    }

    private void OnEnable()
    {
        Debug.Log("Inventory Panel is enabled");
    }

    private void OnDisable()
    {
        Debug.Log("Inventory Panel is disabled");
    }
    // do your funky stuff here
}
