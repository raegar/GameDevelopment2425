/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :A simple script to demonstrate inheritance from the PanelBase.cs script
 */
using UnityEngine;
public class SettlerPanel : PanelBase
{
   protected override void Awake()
    {
        // Dont remove this. This is required to register the panel with the UIManager
        base.Awake();

        // do anything else that you want to do when your script wakes up here
    }

    private void OnEnable()
    {
        Debug.Log("Settler Panel is enabled");
    }

    private void OnDisable()
    {
        Debug.Log("Settler Panel is disabled");
    }
    // do other funky stuff here
}
