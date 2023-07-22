using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : MonoBehaviour
{
    public const float outerRadius = 0.5f;

    public const float innerRadius = outerRadius * 0.866025404f;


    public static Vector3[] unitVertices = {
        new Vector3(2f, 4f, 0f),
        new Vector3(4f, 2f, 0f),
        new Vector3(4f, -2f, 0f),
        new Vector3(2f, -4f, 0f),
        new Vector3(-2, -4f, 0f),
        new Vector3(-4f, -2f, 0f),
        new Vector3(-4f, 2f, 0f),
        new Vector3(-2f, 4f, 0f),
        new Vector3(2f, 4f, 0f),
    };
      

    List<Vector3> vertices;
    List<int> triangles;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        vertices = new List<Vector3>();
        triangles = new List<int>();

        Vector3 center = transform.localPosition;
        for (int i = 0; i < unitVertices.Length -1 ; i++)
        {
            AddTriangle(
                center,
                center + unitVertices[i],
                center + unitVertices[i + 1]
            );
        }


        // Do some calculations...
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}
