using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    int cellNum = 0;

    public int centerX = 0;
    public int centerY = 0;
    public SquareCell cellPrefab;

    int xMax = 9999;
    int xMin = 10;
    int zMax = 48;
    int zMin = 9;

    Vector3 anchorPosition;
    SquareCell[] cells;

    Vector3[] centerVertices;

    SquareMesh squareMesh;
    //SquareLabel squareLabel;
    float squareCellSize = SquareMatrics.length;


    void Awake()
    {
        anchorPosition = gameObject.transform.parent.position;

        squareMesh = GetComponentInChildren<SquareMesh>();
        //squareLabel = GetComponentInChildren<SquareLabel>();

        cellNum = 0;
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x > xMin && x < xMax && z > zMin && z < zMax)
                {
                    continue;
                }

                if (x > 42 && x < 9999 && z > 47 && z < 61)
                {
                    continue;
                }

                if (x > 62 && x < 9999 && z > -9999 && z < 9999)
                {
                    continue;
                }

                if (x > 47 && x < 54 && z > 2 && z < 4)
                {
                    continue;
                }

                cellNum++;
            }
        }

        //cells = new SquareCell[height * width];
        //centerVertices = new Vector3[height * width];

        cells = new SquareCell[cellNum];
        centerVertices = new Vector3[cellNum];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x > xMin && x < xMax && z > zMin && z < zMax)
                {
                    continue;
                }

                if (x > 42 && x < 9999 && z > 47 && z < 61)
                {
                    continue;
                }

                if (x > 62 && x < 9999 && z > -9999 && z < 9999)
                {
                    continue;
                }

                if (x > 47 && x < 54 && z > 2 && z < 4)
                {
                    continue;
                }
                CreateCell(x, z, i++);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        squareMesh.Draw(cells, width, height);
        //squareLabel.Label(width, height, centerX, centerY);
        gameObject.SetActive(false);

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
        position.x = (x - (float)width / 2) * squareCellSize;
        position.y = 0f;
        position.z = (z - (float)height / 2) * squareCellSize;


        //position += anchorPosition;

        SquareCell cell = cells[i] = Instantiate<SquareCell>(cellPrefab);
        cell.coordinate.x = x;
        cell.coordinate.y = z;
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.cellID = i;
        int xReal = x - 6;
        int zReal = z - 3;
        cell.name = "SquareCell_" + xReal + "_" + zReal;
        centerVertices[i] = position;
    }
}
