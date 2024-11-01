using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;


[CreateAssetMenu(fileName = "RegisterPanelSO", menuName = "UIManager/RegisterPanelSO", order = 1)]
public class RegisterPanelSO : ScriptableObject
{
    [SerializedDictionary("Unique Panel Name", "Prefab containing Panel")]
    public SerializedDictionary<string,PanelBase> registeredPanels = new SerializedDictionary<string, PanelBase>();
}
