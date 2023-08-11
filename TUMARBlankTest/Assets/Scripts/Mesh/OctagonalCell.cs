using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonalCell : MonoBehaviour
{
    public float size = 1.0f;

    public static Vector3[] unitVertices = {
        new Vector3(2f, 0f, 4f),
        new Vector3(4f, 0f, 2f),
        new Vector3(4f, 0f, -2f),
        new Vector3(2f, 0f, -4f),
        new Vector3(-2, 0f, -4f),
        new Vector3(-4f, 0f, -2f),
        new Vector3(-4f, 0f, 2f),
        new Vector3(-2f, 0f, 4f),
        new Vector3(2f, 0f, 4f),
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
        for (int i = 0; i < unitVertices.Length - 1; i++)
        {
            vertices.Add(center + unitVertices[i] * size);
        }

        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i+1);
        }

        Vector2[] uv = new Vector2[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            uv[i] = new Vector2(unitVertices[i].x, unitVertices[i].z);
        }

        Vector3[] normals = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            normals[i] = -Vector3.forward;
        }
        // Do some calculations...
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals;
        //mesh.uv = uv;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
