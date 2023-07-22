using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrid : MonoBehaviour
{
    public int circleNum = 12;
    Vector3 anchorPosition;

    public CircleCell cellPrefab;
    CircleCell[] cells;

    Vector3[] centerVertices;

    CircleMesh circleMesh;

    public float basicR = 1.0f;
    public int lineNum = 720;
    public int sectorNum = 36;
    void Awake()
    {
        anchorPosition = gameObject.transform.parent.position;

        circleMesh = GetComponentInChildren<CircleMesh>();

        cells = new CircleCell[circleNum];
        centerVertices = new Vector3[circleNum];
        for (int i = 0; i < circleNum; i++)
        {
            CreateCell(i);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        circleMesh.Draw(cells, basicR, lineNum, sectorNum);
        //Debug.Log("CircleGird.Start()");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateCell(int i)
    {
        Vector3 position = new Vector3();

        //position.y -= anchorPosition.y;

        CircleCell cell = cells[i] = Instantiate<CircleCell>(cellPrefab);

        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.cellID = i;
        cell.name = "CircleCell_" + cell.cellID;

        //MoveWithCamera moveCell = cell.gameObject.AddComponent<MoveWithCamera>();
        //moveCell.cameraMain = ;
        //moveCell.model = transform;
        centerVertices[i] = position;
    }
}
