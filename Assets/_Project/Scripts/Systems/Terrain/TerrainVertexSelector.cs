using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainVertexSelector : MonoBehaviour
{
    public GameObject selectionFramePrefab;
    public List<(List<VertexPositionData> selectedVertices, GameObject frame)> activeManipulators 
      = new List<(List<VertexPositionData> selectedVertices, GameObject frame)>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement()) 
        {
            // Get the nearest vertex on click
            var result = SelectNearestVertex();
            if(result.terrain != null)
            {
                // Check if the vertex is on the edge of the mesh
                List<VertexPositionData> terrainsAndVertices = CheckForNeighbours(result.terrain, result.heightmapPosition);
                // Create a frame at the selected position
                GameObject newFrame = Instantiate(selectionFramePrefab, result.worldPosition, Quaternion.identity);
                // Add the terrain and any neighbours, as well as the frame object, to a list
                activeManipulators.Add((terrainsAndVertices, newFrame));
            }
        }


        if (Input.GetMouseButtonDown(1) && !IsPointerOverUIElement())
        {
            // Perform a raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the object hit is a selection frame
                GameObject hitObject = hit.collider.gameObject;
                for (int i = 0; i < activeManipulators.Count; i++)
                {
                    if (hitObject == activeManipulators[i].frame)
                    {
                        // If it is destroy it and remove it from the list
                        Destroy(hit.collider.gameObject);
                        activeManipulators.RemoveAt(i);
                    }
                }

            }

        } 
    }

    // This is a helper to stop interactions with the terrain when over a UI element
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // This is a helper, used in CheckForNeighbours to reduce repetition
    // Its purpose is to create vertex position data which is added to activeManipulators
    private VertexPositionData CreateVertexPositionData(Terrain terrain, Vector3 position)
    {
        VertexPositionData vertexPositionData = new VertexPositionData();
        vertexPositionData.SetPositionData(terrain, position);
        return vertexPositionData;
    }

    // This is a helper, used in CheckForNeighbours to reduce repetition
    // Its purpose is to find adjacent terrains
    private Terrain GetNeighbourTerrain(Vector3 currentPosition, float offsetX, float offsetZ)
    {
        Vector3 neighborPosition = currentPosition + new Vector3(offsetX, 0, offsetZ);
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            if (terrain.transform.position == neighborPosition)
            {
                return terrain;
            }
        }
        return null;
    }

    // Checks all directions for any adjacent terrains
    private List<VertexPositionData> CheckForNeighbours(Terrain terrain, Vector3 heightmapPosition)
    {
        // List to store all terrains and corresponding vertices
        List<VertexPositionData> vertexPositionDatas = new List<VertexPositionData>();
        // Stores the selected terrain
        vertexPositionDatas.Add(CreateVertexPositionData(terrain, heightmapPosition));
        // 
        int resolutionZeroIndexed = terrain.terrainData.heightmapResolution -1;
        int trueConditions = 0;
        if (heightmapPosition.x == 0)
        {
            Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, 0);
            if(neighbour != null)
            {
                Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y , heightmapPosition.z);
                vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.z == 0)
        {
            Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, 0, -terrain.terrainData.size.z);
            if (neighbour != null)
            {
                Vector3 neighbourVertex = new Vector3(heightmapPosition.x, heightmapPosition.y, resolutionZeroIndexed);
                vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.x == resolutionZeroIndexed)
        {
            Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, 0);
            if (neighbour != null)
            {
                Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, heightmapPosition.z);
                vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.z == resolutionZeroIndexed)
        {
            Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, 0, terrain.terrainData.size.x);
            if (neighbour != null)
            {
                Vector3 neighbourVertex = new Vector3(heightmapPosition.x, heightmapPosition.y, 0);
                vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                trueConditions++;
            }
        }
        // If this is true then its a corner and this gets the diagonally adjacent terrain
        if (trueConditions == 2)
        {
            if (heightmapPosition.x == 0 && heightmapPosition.z == 0)
            {
                // Handle top left corner
                Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, -terrain.terrainData.size.z);
                if (neighbour != null)
                {
                    Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y, resolutionZeroIndexed);
                    vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == 0 && heightmapPosition.z == resolutionZeroIndexed)
            {
                // Handle top right corner
                Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, terrain.terrainData.size.z);
                if (neighbour != null)
                {
                    Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y, 0);
                    vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == resolutionZeroIndexed && heightmapPosition.z == 0)
            {
                // Handle bottom left corner
                Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, -terrain.terrainData.size.z);
                if (neighbour != null)
                {
                    Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, resolutionZeroIndexed);
                    vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == resolutionZeroIndexed && heightmapPosition.z == resolutionZeroIndexed)
            {
                // Handle bottom right corner
                Terrain neighbour = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, terrain.terrainData.size.z);
                if (neighbour != null)
                {
                    Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, 0);
                    vertexPositionDatas.Add(CreateVertexPositionData(neighbour, neighbourVertex));
                }
            }
        }
        return vertexPositionDatas;
    }

    



    private (Terrain terrain, Vector3 heightmapPosition, Vector3 worldPosition) SelectNearestVertex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the object hit contains a terrain
            if (hit.collider.gameObject.GetComponent<Terrain>())
            {
                // Assign hit terrain
                Terrain terrain = hit.collider.gameObject.GetComponent<Terrain>();

                // Convert world position to local position on the terrain
                Vector3 localHitPosition = hit.point - terrain.transform.position;

                // Get the position in terrain heightmap coordinates
                //             (Convert local position to a value between 0 & 1) * (Scale this to the heightmaps resolution - 1) (-1 as grid indices are zero based)
                float coordX = (localHitPosition.x / terrain.terrainData.size.x) * (terrain.terrainData.heightmapResolution - 1);
                float coordZ = (localHitPosition.z / terrain.terrainData.size.z) * (terrain.terrainData.heightmapResolution - 1);

                // Round coordinates to nearest vertex in the heightmap grid
                int nearestX = Mathf.RoundToInt(coordX);
                int nearestZ = Mathf.RoundToInt(coordZ);

                //Debug.Log($"Nearest vertex coordinates: X={nearestX}, Z={nearestZ}");

                // Get the height at this point
                float height = terrain.terrainData.GetHeight(nearestX, nearestZ);

                Vector3 vertexHeightmapPosition = new Vector3(nearestX, height, nearestZ);

                Vector3 vertexWorldPosition = new Vector3(nearestX / ((float)terrain.terrainData.heightmapResolution - 1) * terrain.terrainData.size.x,
                                                          height,
                                                          nearestZ / ((float)terrain.terrainData.heightmapResolution - 1) * terrain.terrainData.size.z)
                                                          + terrain.transform.position;

                //Debug.Log($"Vertex heightmap position: {vertexHeightmapPosition}");
                //Debug.Log($"Vertex world position: {vertexWorldPosition}");

                // Visualize the vertex
                HighlightVertex(vertexWorldPosition);
                return (terrain, vertexHeightmapPosition, vertexWorldPosition);
            }
        }
        return (null, Vector3.zero, Vector3.zero);
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


