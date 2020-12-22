using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    System.Random rnd;

    Mesh mesh;
    MeshCollider collider;

    [HideInInspector]
    public Vector3[] vertices;
    int[] triangles;
    [HideInInspector]
    public Color[] colors;

    int mapX;
    int mapZ;

    public int xSize = 20;
    public int zSize = 20;

    public int xGrow = 10;
    public int zGrow = 10;
    
    public int squareSize = 2;

    public float noiseStrength = 12f;
    public float centralHillStrength = 12f;

    // Start is called before the first frame update
    void Start()
    {
        rnd  = new System.Random();

        mapX = rnd.Next(xSize, xSize + xGrow);
        mapZ = rnd.Next(zSize, zSize + zGrow);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        collider = GetComponent<MeshCollider>();

        createShape();
        updateMesh();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void createShape()
    {
        vertices = new Vector3[(mapX + 1) * (mapZ + 1)];

        float xMiddle = mapX * squareSize / 2;
        float zMiddle = mapZ * squareSize / 2;
        Vector3 peak = new Vector3(Random.Range(xMiddle - 100f, xMiddle + 100f) , 0, Random.Range(zMiddle - 100f, zMiddle + 100f));

        for (int i = 0, z = 0; z <= mapZ; z++)
        {
            for (int x = 0; x <= mapX; x++)
            {
                float dist = Vector3.Distance(peak, new Vector3(x * squareSize, 0, z * squareSize));
                float y = Mathf.PerlinNoise(x * .12f, z * .12f) * noiseStrength * 9  - Mathf.Sqrt(dist * 6 * centralHillStrength) + 130f;
                vertices[i] = new Vector3(x * squareSize, y, z * squareSize);
                i++;
            }
        }



        triangles = new int[6 * mapX * mapZ];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < mapZ; z++)
        {
            for (int x = 0; x < mapX; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + mapX + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + mapX + 1;
                triangles[tris + 5] = vert + mapX + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }
        colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
            colors[i] = Color.Lerp(
                Color.Lerp(
                    CombineColors(Color.white, Color.yellow),
                    CombineColors(Color.green, Color.yellow),
                    (vertices[i].y + 35) / 2),
                Color.Lerp(
                    Color.green, 
                    CombineColors(Color.white, Color.gray),
                    (vertices[i].y - 10) / 3),
                vertices[i].y + 15);


       
    }

    void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        collider.sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }

    /*private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
         for (int i = 0; i < vertices.Length; i++)
            {
            Gizmos.DrawSphere(vertices[i], .1f);
            }
    }*/
}
