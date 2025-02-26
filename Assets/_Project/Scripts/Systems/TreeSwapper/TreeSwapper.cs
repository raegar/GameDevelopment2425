using Inventory2;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using UnityEngine;

public class TreeSwapper : MonoBehaviour
{
   
        public GameObject theTree;
        Terrain[] terrains;
        TerrainData thisTerrain;
        public PlaceableSO placeablesSO;

    // Use this for initialization
    void Start()
    {
        placeablesSO.ClearPlaceables();
        terrains = GetComponentsInChildren<Terrain>();
        for (int i = 0; i < terrains.Length; i++)
        {
            // Grab the island's terrain data
            thisTerrain = terrains[i].terrainData;
     

            // For every tree on the island
            foreach (TreeInstance tree in thisTerrain.treeInstances)
            {
                Debug.Log(tree.prototypeIndex);
                Vector3 worldTreePos = Vector3.Scale(tree.position, thisTerrain.size) + terrains[i].transform.position;
                Instantiate(theTree, worldTreePos, Quaternion.identity); // Create a prefab tree on its pos
                placeablesSO.AddPlaceable(worldTreePos, tree.prototypeIndex); // Add the tree to the list of placeables
            }
            // Then delete all trees on the island
            List<TreeInstance> newTrees = new List<TreeInstance>(0);
            thisTerrain.treeInstances = newTrees.ToArray();
        }
    }
}

