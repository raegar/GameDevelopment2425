using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableSpawner : MonoBehaviour
{
    public PlaceableSO placeableSO;
    // Start is called before the first frame update
    void Start()
    {
        placeableSO.SpawnPlaceables();
    }

}
