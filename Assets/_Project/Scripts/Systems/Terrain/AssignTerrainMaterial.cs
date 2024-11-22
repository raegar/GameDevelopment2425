using UnityEngine;

namespace terrain
{
    public class AssignTerrainMaterial : MonoBehaviour
    {
        public Material customMaterial;

        void Start()
        {
            Terrain[] terrains = FindObjectsOfType<Terrain>();

            foreach (Terrain terrain in terrains)
            {
                terrain.materialTemplate = customMaterial;
            }
        }

        public void UpdateTextures()
        {
            Terrain[] terrains = FindObjectsOfType<Terrain>();

            foreach (Terrain terrain in terrains)
            {
                terrain.materialTemplate = customMaterial;
            }
        }

    }
}

