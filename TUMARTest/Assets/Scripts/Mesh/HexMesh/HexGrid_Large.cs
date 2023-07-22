using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid_Large : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.red;

    Vector3 anchorPosition;
    HexCell[] cells;

    Vector3[] centerVertices;

    //Canvas gridCanvas;
    HexMesh_Large hexMesh;

    float hexCellSize = HexMatrics_Large.outerRadius;


    void Awake()
    {
        //gridCanvas = GetComponentInChildren<Canvas>();

        anchorPosition = gameObject.transform.parent.position;


        hexMesh = GetComponentInChildren<HexMesh_Large>();

        cells = new HexCell[height * width];
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
        hexMesh.Draw(cells);

        //gameObject.SetActive(false);

        //hexMesh.Triangulate(cells);
        //hexMesh.Triangulate(centerVertices);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x - (float)width / 2) * hexCellSize * 2.0f;
        position.y = 0.0f;
        position.z = (z - (float)height / 2) * hexCellSize * 1.5f + 0.625f;

        position.x = ((x - (float)width / 2) + z * 0.5f - z / 2) * (HexMatrics_Large.innerRadius * 2f);

        //position += anchorPosition;

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);

        cell.color = defaultColor;

        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.cellID = i;
        cell.name = "HexLargeCell_" + cell.cellID;
        centerVertices[i] = position;
    }

}