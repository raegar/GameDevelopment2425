using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDropOff : MonoBehaviour
{
    public float dropOffDistance = 500f;
    public float secondDropOffDistance = 510f;
    public float fullDropOffDistance = 520f;

    // Start is called before the first frame update
    void Start()
    {
        // Get all active terrains in the scene
        Terrain[] terrains = Terrain.activeTerrains;
        foreach (Terrain terrain in terrains)
        {
            
            TerrainData tData = terrain.terrainData;
            int res = tData.heightmapResolution;
            float[,] heights = tData.GetHeights(0, 0, res, res);

            // Calculate normalized height reduction for a 1 world unit change
            float heightReductionNormalized = 1f / tData.size.y;

            // Loop through each vertex (height sample)
            for (int z = 0; z < res; z++)
            {
                for (int x = 0; x < res; x++)
                {
                    // Convert the heightmap coordinate to world space (x, z)
                    float worldX = terrain.transform.position.x + ((float)x / (res - 1)) * tData.size.x;
                    float worldZ = terrain.transform.position.z + ((float)z / (res - 1)) * tData.size.z;
                    Vector2 vertexPos = new Vector2(worldX, worldZ);

                    // Check if this vertex is more than 500 units from the origin
                    if (vertexPos.magnitude > dropOffDistance)
                    {
                        // Reduce the vertex height by 1 unit (normalized)
                        heights[z, x] = Mathf.Max(heights[z, x] - heightReductionNormalized, 0f);
                    }
                    if (vertexPos.magnitude > secondDropOffDistance)
                    {
                        // Reduce the vertex height by 1 unit (normalized)
                        heights[z, x] = Mathf.Max(heights[z, x] - heightReductionNormalized, 0f);
                    }
                    if (vertexPos.magnitude > fullDropOffDistance)
                    {
                        // Reduce the vertex height by 1 unit (normalized)
                        heights[z, x] = 0f;
                    }
                }
            }

            // Apply the modified heights back to the terrain
            tData.SetHeights(0, 0, heights);
            
        }
        Debug.Log("Terrain vertices modified.");
    }

    
}
