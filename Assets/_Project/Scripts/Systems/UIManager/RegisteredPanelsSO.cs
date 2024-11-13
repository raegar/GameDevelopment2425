/* Author(s)    : Don MacSween 
 * email(s)     : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :This script creates a scriptable oblect with a dictionary containing
 *               all the panels to be used in the game, used by the UIManager on Awake to instantiate them
 *               the created ScriptableObject should reside in the GameData folder and be attached to the UIManager component
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "RegisterPanelsSO", menuName = "UIManager/RegisterPanelSO", order = 1)]
public class RegisteredPanelsSO : ScriptableObject
{
    [Header("Please ensure all panel names are unique")]
    [SerializedDictionary("Unique Panel Name", "Prefab containing Panel")]
    public SerializedDictionary<string,PanelBase> panels = new SerializedDictionary<string, PanelBase>();  
}
