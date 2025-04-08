using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Placeable/PlaceableSO", order = 1)]
public class PlaceableSO : ScriptableObject
{
    public List<GameObject> placeables;
    public List<Vector3> placeablePosition;
    public List<int> placeableIndex = new List<int>();
    public float yCutOff = 0f;

    public void ClearPlaceables()
    {
        placeablePosition.Clear();
        placeableIndex.Clear();
    }

    public void AddPlaceable(Vector3 xyz, int index)
    {
        
        placeablePosition.Add(xyz);
        placeableIndex.Add(index);
    }

    public void SpawnPlaceables()
    {
        
        //Debug.Log("num placeables = " + placeablePosition.Count);
        for (int i = 0; i < placeablePosition.Count -1; i++)
        {
            try { 
            //Debug.Log("placeable: "+ placeableIndex[i]);
            if (placeablePosition[i].y > yCutOff)
                {
                    Instantiate(placeables[placeableIndex[i]], placeablePosition[i], Quaternion.identity);
                }
                
            } catch
            {

            }
        }
    }
}

