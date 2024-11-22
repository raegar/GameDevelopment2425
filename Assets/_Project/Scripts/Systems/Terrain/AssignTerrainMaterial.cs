using UnityEngine;

public class AssignTerrainMaterial : MonoBehaviour
{
    public Terrain terrain;
    public Terrain terrain1;
    public Terrain terrain2;
    public Terrain terrain3;
    public Material customMaterial;

    void Start()
    {
        if (terrain != null && customMaterial != null)
        {
            terrain.materialTemplate = customMaterial;
            terrain1.materialTemplate = customMaterial;
            terrain2.materialTemplate = customMaterial;
            terrain3.materialTemplate = customMaterial;
        }
    }
}
