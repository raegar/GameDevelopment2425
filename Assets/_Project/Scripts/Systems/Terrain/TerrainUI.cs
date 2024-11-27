using UnityEngine;

namespace terrain
{
    public class TerrainUI : MonoBehaviour
    {
        TerrainVertexSelector terrainVertexSelector;

        // Start is called before the first frame update
        void Start()
        {
            terrainVertexSelector = GetComponent<TerrainVertexSelector>();
        }
        public void RaiseTerrain()
        {
            ModifyTerrainHeight(1);
        }
        public void LowerTerrain()
        {
            ModifyTerrainHeight(-1);
        }

        private void ModifyTerrainHeight(int value)
        {
            // Use this to clamp the max height of the frame
            float scaledNewHeight = 0;
            for (int i = 0; i < terrainVertexSelector.activeManipulators.Count; i++)
            {
                for (int j = 0; j < terrainVertexSelector.activeManipulators[i].selectedVertices.Count; j++)
                {
                    // Get Height (Stored as whole number)
                    float currentHeight = terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.y;
                    // Modify height value
                    float newHeight = Mathf.Round(currentHeight + value);
                    // Normalise height 
                    float normalizedNewHeight = Mathf.Clamp(newHeight / terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain.terrainData.size.y, 0f, 1f);


                    // Apply new height to heightmap
                    terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain.terrainData.SetHeights((int)terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.x,
                                                                                                                   (int)terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.z,
                                                                                                                   new float[,] { { normalizedNewHeight } });
                    // Reassign modified height value to this list
                    scaledNewHeight = normalizedNewHeight * terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain.terrainData.size.y;
                    VertexPositionData vertexPositionData = new VertexPositionData();
                    Vector3 vertexPosition = new Vector3(terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.x,
                                                         scaledNewHeight,
                                                         terrainVertexSelector.activeManipulators[i].selectedVertices[j].vertex.z);

                    vertexPositionData.SetPositionData(terrainVertexSelector.activeManipulators[i].selectedVertices[j].terrain, vertexPosition);
                    terrainVertexSelector.activeManipulators[i].selectedVertices[j] = vertexPositionData;
                }
                // Update the frames position relative to the new height position
                Vector3 framePosition = terrainVertexSelector.activeManipulators[i].frame.transform.position;
                framePosition.y = scaledNewHeight;
                terrainVertexSelector.activeManipulators[i].frame.transform.position = framePosition;

                // Repaint the terrain
                AssignTerrainMaterial terrainMaterial = GetComponent<AssignTerrainMaterial>();
                terrainMaterial.UpdateTextures();
                //TerrainPainter tPainter = GetComponent<TerrainPainter>();
                //tPainter.RepaintAll();
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
}
