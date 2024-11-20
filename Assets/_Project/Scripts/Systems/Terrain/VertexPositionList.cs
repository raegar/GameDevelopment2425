using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionList : MonoBehaviour
{
    private List<VertexPositionData> vertexPositionDatas = new List<VertexPositionData>();
    private Vector3 worldPosition;

    public void AddData(VertexPositionData data) { vertexPositionDatas.Add(data); }
    public void SetWorldPosition(Vector3 position) { worldPosition = position; }
}
