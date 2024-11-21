using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionList
{
    public List<VertexPositionData> vertexPositionDatas { get; private set; } = new List<VertexPositionData>();
    public GameObject frame { get; private set; }

    public void SetListData(VertexPositionData data, GameObject frame)
    {
        vertexPositionDatas.Add(data);
        this.frame = frame;
    }

}
