using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainUI : MonoBehaviour
{

    TerrainVertexSelector terrainVertexSelector;


    // Start is called before the first frame update
    void Start()
    {
        terrainVertexSelector = GetComponent<TerrainVertexSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseTerrain()
    {
        foreach (var manipulator in terrainVertexSelector.activeManipulators)
        {
            foreach (var vertexData in manipulator.selectedVertices)
            {
                float currentHeight = vertexData.vertex.y;
                float sizeY = vertexData.terrain.terrainData.size.y;
                float currentHeightScaled = currentHeight * sizeY;
                float newHeightScaled = currentHeightScaled + 1;
                float normalizedNewHeight = newHeightScaled / sizeY;
                Terrain currentTerrain = vertexData.terrain;
                float[,] target = vertexData.terrain.terrainData.GetHeights((int)vertexData.vertex.x , (int)vertexData.vertex.z , 1 , 1 );
                target[0,0] = normalizedNewHeight;
                vertexData.terrain.terrainData.SetHeights((int)vertexData.vertex.x, (int)vertexData.vertex.z, target);
                // This is a mess and doesnt work properly
                // Also need to do batch updates per terrain instead of doing multiple updates on the same terrain
            }
        }
    }

    public void ClearSelection()
    {
        foreach (var manipulator in terrainVertexSelector.activeManipulators)
        {
            Destroy(manipulator.frame);
        }
        terrainVertexSelector.activeManipulators.Clear();
    }
}
