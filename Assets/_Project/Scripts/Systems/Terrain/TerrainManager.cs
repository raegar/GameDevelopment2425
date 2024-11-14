using sc.terrain.proceduralpainter;
using UnityEngine;

namespace terrain
{
    public class TerrainManager : MonoBehaviour
    {
        [Tooltip("Number of terrain tiles.")]
        [Min(1)]
        [SerializeField] private int gridSizeX = 3;
        [Tooltip("Number of terrain tiles.")]
        [Min(1)]
        [SerializeField] private int gridSizeZ = 3;
        [Tooltip("Size of the square terrain.")]
        [Min(16)]
        [SerializeField] private int terrainSize = 64;
        [Tooltip("Maximum height of the terrain.")]
        [Min(1)]
        [SerializeField] private float maxY = 5.0f;
        [Tooltip("Controls the scale (Aka zoom) of the Perlin noise.")]
        [SerializeField] private float scale = 25.0f;
        [Tooltip("Set a 'random' seed to create a unique pattern.")]
        [SerializeField] private int seed = 0;


        // Start is called before the first frame update
        void Start()
        {
            Terrain terrain = FindAnyObjectByType<Terrain>();
            if (terrain == null)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    for (int z = 0; z < gridSizeZ; z++)
                    {
                        TerrainData terrainData = new TerrainData();
                        GameObject newTerrain = Terrain.CreateTerrainGameObject(terrainData);
                        PerlinNoiseGenerator generator = newTerrain.AddComponent<PerlinNoiseGenerator>();
                        generator.ConfigureGenerator(terrainSize, maxY, scale, x * terrainSize, z * terrainSize, seed);
                        newTerrain.transform.position = new Vector3(x * terrainSize, 0, z * terrainSize);

                    }
                }
            }
            else
            {
                Debug.LogWarning("Terrain detected in scene, Procedural generation halted");
            }

            

            // Repaint the terrain after the first frame
            Invoke("RepaintTerrain", 0f);
        }

        private void RepaintTerrain()
        {
            TerrainPainter TPainter = GetComponent<TerrainPainter>();
            TPainter.AssignActiveTerrains();
            TPainter.RepaintAll();
        }

    }
}
