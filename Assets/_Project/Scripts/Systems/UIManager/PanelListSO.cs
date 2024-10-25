using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "panelList", menuName = "GameData/UIPanelList", order = 1)]
public class PanelListSO : ScriptableObject
{
    public SerializedDictionary<PanelBase, bool> listOfPanels;
}
