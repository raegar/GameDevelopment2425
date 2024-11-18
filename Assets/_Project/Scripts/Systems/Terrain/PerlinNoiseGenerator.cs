using UnityEngine;


namespace terrain {
    public class PerlinNoiseGenerator : MonoBehaviour
    {
        private Terrain terrain;
        [SerializeField] private int terrainSize = 64;
        [SerializeField] private float maxY = 5.0f;
        [SerializeField] private float scale = 25.0f;
        [SerializeField] private float offSetX = 0.0f;
        [SerializeField] private float offSetZ = 0.0f;
        [SerializeField] private int seed = 0;

        private void Start()
        {
            terrain = GetComponent<Terrain>();
            GenerateNoise();
        }

        public void ConfigureGenerator(int terrainSize, float maxY, float scale, float offSetX, float offSetZ, int seed)
        {
            this.terrainSize = terrainSize;
            this.maxY = maxY;
            this.scale = scale;
            this.offSetX = offSetX;
            this.offSetZ = offSetZ;
            this.seed = seed;
    }


        private void GenerateNoise()
        {
            TerrainData terrainData = terrain.terrainData;

            // The heightmap resolution denotes the number of "vertices" in the terrain
            // If you were to double the resolution then there would be twice as many "vertices" along an axis
            // We set the heightmap resolution to match the terrain size (+1 for full grid coverage) to get a 1m x 1m block effect
            terrainData.heightmapResolution = terrainSize + 1;

            // Set the size of the terrain in meters
            terrainData.size = new Vector3(terrainSize, maxY, terrainSize);

            // Create an array of heights to store the "vertices"
            float[,] heights = new float[terrainSize + 1, terrainSize + 1];

            // Iterate through each "vertex" position in the heightmap grid
            for (int z = 0; z <= terrainSize; z++)
            {
                for (int x = 0; x <= terrainSize; x++)
                {
                    // Generate noise -          Index + tiled offset + any seed value / scale ... finally multiply by maxY (See end of loop to explain this)
                    float y = Mathf.PerlinNoise((x + offSetZ + (seed * 100)) / scale, (z + offSetX + (seed * 100)) / scale) * maxY;

                    // Round height for blocky effect
                    float height = Mathf.Floor(y);

                    // Normalize height between 0 and 1 (Unity's heightmap format requires values between 0 and 1)
                    heights[x, z] = height / terrainData.size.y;

                    // The Perlin noise function returns a value between 0 and 1 so we * maxY making this a number between 0 and maxY
                    // And then floor result to make it a whole number
                    // And finally / by terrainData.y to return this to a decimal between 0 and 1 (terrainData.y and maxY are mathmatically the same)
                    // This causes any number between 0.2 and 0.39 to be 0.2 (if maxY = 5) and gives the blocky asthetic 
                }
            }

            // Apply heights to the TerrainData
            // TerrainData.SetHeights(int xBase, int yBase, float[,] heights);
            // Int xBase and yBase denote the starting position in the heightmap there the new data will be applied.
            terrainData.SetHeights(0, 0, heights);
        }
    }
}
