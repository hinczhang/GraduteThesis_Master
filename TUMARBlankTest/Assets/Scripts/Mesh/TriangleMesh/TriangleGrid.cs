using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public TriangleCell cellPrefab;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.red;

    Vector3 anchorPosition;
    TriangleCell[] cells;

    Vector3[] centerVertices;

    //Canvas gridCanvas;
    TriangleMesh triangleMesh;

    float triangleCellSize = TriangleMatrics.outerRadius;


    void Awake()
    {
        //gridCanvas = GetComponentInChildren<Canvas>();

        anchorPosition = gameObject.transform.parent.position;


        triangleMesh = GetComponentInChildren<TriangleMesh>();

        cells = new TriangleCell[height * width];
        centerVertices = new Vector3[height * width];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        triangleMesh.Draw(cells);
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x - (float)width / 2) * triangleCellSize * 2.0f;
        position.y = 0f;
        position.z = (z - (float)height / 2) * triangleCellSize * 1.5f;
        
        position.x = ((x - (float)width / 2) + z * 0.5f - z / 2) * (TriangleMatrics.innerRadius * 2f);

        //position += anchorPosition;

        TriangleCell cell = cells[i] = Instantiate<TriangleCell>(cellPrefab);

        cell.color = defaultColor;

        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        centerVertices[i] = position;
    }
}
