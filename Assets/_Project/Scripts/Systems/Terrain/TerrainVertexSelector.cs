using GluonGui.Dialog;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVertexSelector : MonoBehaviour
{
    List<GameObject> terrainsHit = new List<GameObject>();
    Vector3 hitPoint;
    VertexPositionList vertexPositionList = new VertexPositionList();


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            PerformRaycast();
            foreach (GameObject terrain in terrainsHit)
            {
                VertexPositionData vertexPositionData = new VertexPositionData();
                var result = SelectNearestVertex(terrain);
                vertexPositionData.SetPositionData(terrain, result.heightMap);
                vertexPositionList.AddData(vertexPositionData);
                vertexPositionList.SetWorldPosition(result.world);
            }
            
        }
    }

    private void PerformRaycast()
    {
        // Raycast to detect the terrain
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the object hit contains a terrain
            if (hit.collider.gameObject.GetComponent<Terrain>())
            {
                hitPoint = hit.point;
                // Check for multiple terrains
                CheckNumberOfTerrainsHit(ray);
            }
        }
    }

    private void CheckNumberOfTerrainsHit(Ray ray)
    {
        // Check all raycast hits
        RaycastHit[] raycastHits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in raycastHits)
        {
            if (hit.collider.gameObject.GetComponent<Terrain>())
            {
                // Add each terrain hit to the List 
                // This is so we can check for any overlapping terrain boarders
                terrainsHit.Add(hit.collider.gameObject);
            }
        }
    }

    private (Vector3 heightMap , Vector3 world) SelectNearestVertex(GameObject terrainHit)
    {
        // Assign hit terrain
        Terrain terrain = terrainHit.GetComponent<Terrain>();

        // Convert world position to local position on the terrain
        Vector3 localHitPosition = hitPoint - terrain.transform.position;

        // Get the position in terrain heightmap coordinates
        //             (Convert local position to a value between 0 & 1) * (Scale this to the heightmaps resolution - 1) (-1 as grid indices are zero based)
        float coordX = (localHitPosition.x / terrain.terrainData.size.x) * (terrain.terrainData.heightmapResolution - 1);
        float coordZ = (localHitPosition.z / terrain.terrainData.size.z) * (terrain.terrainData.heightmapResolution - 1);

        // Round coordinates to nearest vertex in the heightmap grid
        int nearestX = Mathf.RoundToInt(coordX);
        int nearestZ = Mathf.RoundToInt(coordZ);

        Debug.Log($"Nearest vertex coordinates: X={nearestX}, Z={nearestZ}");

        // Get the height at this point
        float height = terrain.terrainData.GetHeight(nearestX, nearestZ);

        Vector3 vertexHeightmapPosition = new Vector3(nearestX, height, nearestZ);

        Vector3 vertexWorldPosition = new Vector3(nearestX / (terrain.terrainData.heightmapResolution - 1) * terrain.terrainData.size.x,
                                                    height,
                                                    nearestZ / (terrain.terrainData.heightmapResolution - 1) * terrain.terrainData.size.z)
                                                    + terrain.transform.position;

        Debug.Log($"Vertex heightmap position: {vertexHeightmapPosition}");
        Debug.Log($"Vertex world position: {vertexWorldPosition}");

        // Visualize the vertex
        HighlightVertex(vertexWorldPosition);

        return (vertexHeightmapPosition, vertexWorldPosition);
    }
        
    

    void HighlightVertex(Vector3 vertexPosition)
    {
        // Creates a small sphere to mark the vertex
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = vertexPosition;
        marker.transform.localScale = Vector3.one * 0.5f; // Adjust size as needed
        Destroy(marker, 1f); // Destroy the marker after a short time
    }
}


