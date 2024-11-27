using UnityEngine;

public class VertexPositionData
{
    public Terrain terrain { get; private set; }
    public Vector3 vertex { get; private set; }

    public void SetPositionData(Terrain terrain, Vector3 vertex)
    {
        this.terrain = terrain;
        this.vertex = vertex;
    }


}
