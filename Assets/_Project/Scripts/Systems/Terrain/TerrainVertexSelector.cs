using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainVertexSelector : MonoBehaviour
{
    public GameObject selectionFramePrefab;
    private List<(List<(Terrain terrain, Vector3 vertex)> selectedVertices, GameObject frame, Vector3 worldPosition)> activeManipulators 
      = new List<(List<(Terrain terrain, Vector3 vertex)> selectedVertices, GameObject frame, Vector3 worldPosition)>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement()) 
        {
            var result = SelectNearestVertex();
            if(result.terrain != null)
            {
                var terrainsAndVertices = CheckForNeighbours(result.terrain, result.heightmapPosition);
                Debug.Log(terrainsAndVertices.Count);
                GameObject newFrame = Instantiate(selectionFramePrefab, result.worldPosition, Quaternion.identity);
                activeManipulators.Add((terrainsAndVertices, newFrame, result.worldPosition));


                foreach (var manipulator in activeManipulators)
                {
                    foreach (var vertex in manipulator.selectedVertices)
                    {
                        Debug.Log(vertex.terrain);
                        Debug.Log(vertex.vertex);
                    }
                    Debug.Log(manipulator.frame);
                    Debug.Log(manipulator.worldPosition);
                }
            }
        }


        if (Input.GetMouseButtonDown(1) && !IsPointerOverUIElement())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log(hitObject);
                for (int i = 0; i < activeManipulators.Count; i++)
                {
                    if (hitObject == activeManipulators[i].frame)
                    {
                        Destroy(hit.collider.gameObject);
                        activeManipulators.RemoveAt(i);
                    }
                }

            }

        } 
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void ClearSelection()
    {
        foreach(var manipulator in activeManipulators)
        {
            Destroy(manipulator.frame);
        }
        activeManipulators.Clear();
    }

    private List<(Terrain terrain, Vector3 vertex)> CheckForNeighbours(Terrain terrain, Vector3 heightmapPosition)
    {
        List<(Terrain terrain, Vector3 vertex)> terrainsAndVertices = new List<(Terrain terrain, Vector3 vertex)>();
        terrainsAndVertices.Add((terrain, heightmapPosition));
        int resolutionZeroIndexed = terrain.terrainData.heightmapResolution -1;
        int trueConditions = 0;
        if (heightmapPosition.x == 0)
        {
            Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, 0);
            if(neighbor != null)
            {

                //find corrisponding neighbor vertices
                Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y , heightmapPosition.z);
                terrainsAndVertices.Add((neighbor, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.z == 0)
        {
            Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, 0, -terrain.terrainData.size.z);
            if (neighbor != null)
            {
                Vector3 neighbourVertex = new Vector3(heightmapPosition.x, heightmapPosition.y, resolutionZeroIndexed);
                terrainsAndVertices.Add((neighbor, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.x == resolutionZeroIndexed)
        {
            Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, 0);
            if (neighbor != null)
            {
                Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, heightmapPosition.z);
                terrainsAndVertices.Add((neighbor, neighbourVertex));
                trueConditions++;
            }
        }
        if (heightmapPosition.z == resolutionZeroIndexed)
        {
            Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, 0, terrain.terrainData.size.x);
            if (neighbor != null)
            {
                Vector3 neighbourVertex = new Vector3(heightmapPosition.x, heightmapPosition.y, 0);
                terrainsAndVertices.Add((neighbor, neighbourVertex));
                trueConditions++;
            }
        }

        if (trueConditions == 2)
        {
            //handle corner
            if (heightmapPosition.x == 0 && heightmapPosition.z == 0)
            {
                //handle top left
                Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, -terrain.terrainData.size.z);
                if (neighbor != null)
                {
                    Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y, resolutionZeroIndexed);
                    terrainsAndVertices.Add((neighbor, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == 0 && heightmapPosition.z == resolutionZeroIndexed)
            {
                //handle top right
                Debug.Log($"Top right");
                Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, -terrain.terrainData.size.x, terrain.terrainData.size.z);
                if (neighbor != null)
                {
                    Vector3 neighbourVertex = new Vector3(resolutionZeroIndexed, heightmapPosition.y, 0);
                    terrainsAndVertices.Add((neighbor, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == resolutionZeroIndexed && heightmapPosition.z == 0)
            {
                //handle bottom left
                Debug.Log($"bottom left");
                Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, -terrain.terrainData.size.z);
                if (neighbor != null)
                {
                    Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, resolutionZeroIndexed);
                    terrainsAndVertices.Add((neighbor, neighbourVertex));
                }
            }
            else if (heightmapPosition.x == resolutionZeroIndexed && heightmapPosition.z == resolutionZeroIndexed)
            {
                //handle bottom right
                Debug.Log($"bottom Right");
                Terrain neighbor = GetNeighbourTerrain(terrain.transform.position, terrain.terrainData.size.x, terrain.terrainData.size.z);
                if (neighbor != null)
                {
                    Vector3 neighbourVertex = new Vector3(0, heightmapPosition.y, 0);
                    terrainsAndVertices.Add((neighbor, neighbourVertex));
                }
            }

        }

        return terrainsAndVertices;
    }

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


