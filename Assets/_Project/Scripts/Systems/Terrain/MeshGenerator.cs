using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    [SerializeField] private int xSize = 100;
    [SerializeField] private int zSize = 100;
    [SerializeField] private int maxY = 5;
    [SerializeField] private float scale = 20f;
    [SerializeField] private int offSetX = 0;
    [SerializeField] private int offSetZ = 0;

    private float previousXSize;
    private float previousZSize;
    private float previousMaxY;
    private float previousScale;
    private float previousOffSetX;
    private float previousOffSetZ;


    private void Start()
    {
        GenerateMesh();
        
        previousXSize = xSize;
        previousZSize = zSize;
        previousMaxY = maxY;
        previousScale = scale;
        previousOffSetX = offSetX;
        previousOffSetZ = offSetZ;
    }

    //This currently serves as a developer tool to modify terrain at runtime
    void Update()
    {
        if ( xSize != previousXSize || zSize != previousZSize || maxY != previousMaxY || scale != previousScale || offSetX != previousOffSetX || offSetZ != previousOffSetZ)
        {
            GenerateMesh();
            
            previousXSize = xSize;
            previousZSize = zSize;
            previousMaxY = maxY;
            previousScale = scale;
            previousOffSetX = offSetX;
            previousOffSetZ = offSetZ;
        }
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        GenerateVerticesWithNoise();
        GenerateTriangles();
        GenerateUvs();
        UpdateMeshData();
        ApplyMeshData();
    }

    private void ApplyMeshData()
    {
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void UpdateMeshData()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    private void GenerateVerticesWithNoise()
    {
        //initialise array with the total number of vertices in the mesh 
        //this is used to store each vertices location
        //this is +1 because a 3x2 grid would have 4 verts on x and 3 on Z to make 6 full squares
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //itterate through each vertex
        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //apply some noise to each vertex
                float y = Mathf.PerlinNoise((x + offSetX) / scale, (z + offSetZ) / scale) * maxY;

                if (z > zSize - (zSize / 5))  // Define ocean on one end of the terrain
                {
                    y = Mathf.Min(y, 1);  // if in ocean area make Y 1 or 0
                }
                else if (z > zSize - (zSize / 4))
                {
                    //use default value
                }
                else
                {
                    y = Mathf.Max(y, 2); //if not in ocean make Y 2 or larger
                }

                //round height down to give a more "blocky" asthetic, Removing this line will make the terrain more smooth
                float height = Mathf.Floor(y);

                //Add vertex to the array
                vertices[i] = new Vector3(x, height, z);
                i++;
            }
        }
    }

    private void GenerateTriangles()
    {
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        //itterate through each full square on the mesh
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; ++x)
            {
                //create the first triangle
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;

                //create the second triangle
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                //WARNING: The order of the vertices used in triangle generation is important, each triangle must be generated in a clockwise direction.
                //otherwise you may be in a situation where you can only see one half of a completed square from each side.
                //this is due to backface culling.

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void GenerateUvs()
    {
        uvs = new Vector2[vertices.Length];
        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2(x, z);
                i++;
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for(int i  = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

}
