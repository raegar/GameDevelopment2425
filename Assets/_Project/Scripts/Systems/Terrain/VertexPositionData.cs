using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionData : MonoBehaviour
{
    private GameObject terrainObject;
    private Vector3 terrainPosition;

    public void SetPositionData(GameObject terrain, Vector3 location)
    {
        terrainObject = terrain;
        terrainPosition = location;
    }


}
