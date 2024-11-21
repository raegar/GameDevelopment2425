using sc.terrain.proceduralpainter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        ModifyTerrainHeight(1);
    }
    public void LowerTerrain()
    {
        ModifyTerrainHeight(-1);
    }

    //private void ModifyTerrainHeight(int value)
    //{
    //    TerrainPainter tPainter = GetComponent<TerrainPainter>();

    //    foreach (var manipulator in terrainVertexSelector.activeManipulators)
    //    {
    //        foreach (var vertexData in manipulator.selectedVertices)
    //        {
    //            // Get Height (Stored as whole number)
    //            float currentHeight = vertexData.vertex.y;
    //            // Modify height value
    //            float newHeight = currentHeight + value;
    //            // Normalise height 
    //            float normalizedNewHeight = newHeight / vertexData.terrain.terrainData.size.y;
    //            // Apply new height to heightmap
    //            vertexData.terrain.terrainData.SetHeights((int)vertexData.vertex.x, (int)vertexData.vertex.z, new float[,] { { normalizedNewHeight } });
    //            // Reassign modified height value
    //            vertexData.vertex.y = newHeight;
    //        }

    //        Vector3 framePosition = manipulator.frame.transform.position;
    //        framePosition.y = framePosition.y + value;
    //        manipulator.frame.transform.position = framePosition;
    //        //manipulator.worldPosition = new Vector3(manipulator.worldPosition.x, manipulator.worldPosition.y + 1 , manipulator.worldPosition.z);
    //    }

    //    tPainter.RepaintAll();
    //}

    private void ModifyTerrainHeight(int value)
    {
        TerrainPainter tPainter = GetComponent<TerrainPainter>();
        for (int i = 0; i < terrainVertexSelector.activeManipulators.Count; i++)
        {
            for (int j = 0; j < terrainVertexSelector.activeManipulators[i].selectedVertices.Count; j++)
            {
                // Get Height (Stored as whole number)
                float currentHeight = terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.y;
                // Modify height value
                float newHeight = currentHeight + value;
                // Normalise height 
                float normalizedNewHeight = newHeight / terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain.terrainData.size.y;
                // Apply new height to heightmap
                terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain.terrainData.SetHeights((int)terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.x,
                                                                                                               (int)terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.z,
                                                                                                               new float[,] { { normalizedNewHeight } });
                // Reassign modified height value
                VertexPositionData vertexPositionData = new VertexPositionData();
                Vector3 vertexPosition = new Vector3(terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.x,
                                                     newHeight, 
                                                     terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.z);

                vertexPositionData.SetPositionData(terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain, vertexPosition);
                terrainVertexSelector.activeManipulators[i].selectedVertices[j] = vertexPositionData;
            }

            Vector3 framePosition = terrainVertexSelector.activeManipulators[i].frame.transform.position;
            framePosition.y = framePosition.y + value;
            terrainVertexSelector.activeManipulators[i].frame.transform.position = framePosition;
            //manipulator.worldPosition = new Vector3(manipulator.worldPosition.x, manipulator.worldPosition.y + 1 , manipulator.worldPosition.z);

            tPainter.RepaintAll();

        }

    }


    public void ClearSelection()
    {
        foreach (var manipulator in terrainVertexSelector.activeManipulators)
        {
            Destroy(manipulator.frame.gameObject);
        }
        terrainVertexSelector.activeManipulators.Clear();
    }
}
