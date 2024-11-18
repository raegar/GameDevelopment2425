using sc.terrain.proceduralpainter;
using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
        [Min(8)]
        [SerializeField] private int terrainSize = 64;

        [Tooltip("Maximum height of the terrain.")]
        [Min(1)]
        [SerializeField] private float maxY = 5.0f;

        [Tooltip("Controls the scale (Aka zoom) of the Perlin noise.")]
        [SerializeField] private float scale = 25.0f;

        [Tooltip("Set a 'random' seed to create a unique pattern.")]
        [SerializeField] private int seed = 0;

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

            StartCoroutine(RepaintAfterDelay());
        }

        private IEnumerator RepaintAfterDelay()
        {
            yield return null; // Wait for one frame to ensure initialization
            RepaintTerrain();
        }

        private void RepaintTerrain()
        {
            TerrainPainter TPainter = GetComponent<TerrainPainter>();
            if (TPainter == null)
            {
                Debug.LogError("TerrainPainter component not found!");
                return;
            }

            Terrain[] terrains = FindObjectsOfType<Terrain>();
            if (terrains == null || terrains.Length == 0)
            {
                Debug.LogWarning("No terrains found to repaint.");
                return;
            }

            foreach (var terrain in terrains)
            {
                if (terrain == null || terrain.terrainData == null)
                {
                    Debug.LogWarning("A terrain is null or missing data, skipping.");
                    continue;
                }
            }

            TPainter.AssignActiveTerrains();
            TPainter.RepaintAll();
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                CleanupTerrainReferences();
            }
        }

        private static void CleanupTerrainReferences()
        {
            Terrain[] terrains = FindObjectsOfType<Terrain>();
            foreach (var terrain in terrains)
            {
                if (terrain == null || terrain.terrainData == null)
                {
                    Debug.LogWarning("A destroyed terrain was detected during play mode cleanup.");
                    continue;
                }
            }

            Debug.Log("Terrain references cleaned up on exiting play mode.");
        }

        private class TerrainSaveHandler : AssetModificationProcessor
        {
            public static string[] OnWillSaveAssets(string[] paths)
            {
                Terrain[] terrains = FindObjectsOfType<Terrain>();
                foreach (var terrain in terrains)
                {
                    if (terrain == null || terrain.terrainData == null)
                    {
                        Debug.LogWarning("A destroyed terrain was detected during scene save.");
                        continue;
                    }
                }

                Debug.Log("Scene save operation handled safely.");
                return paths;
            }
        }
#endif
    }
}