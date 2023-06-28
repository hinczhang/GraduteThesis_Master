using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMesh : MonoBehaviour
{


    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    public float width = 0.02f;
    public int numVertices = 2;
    public Material material = null;

    public CellEdge edgePrefab;
    CellEdge[] edges;
    List<Vector3[]> lines;
    List<Vector3[]> linesHistory;

    void Awake()
    {
        lines = new List<Vector3[]>();
        linesHistory = new List<Vector3[]>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw(TriangleCell[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Draw(cells[i]);
        }

        linesHistory.Clear();
    }

    void Draw(TriangleCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        lines.Clear();

        for (int i = 0; i < TriangleMatrics.corners.Length - 1; i++)
        {
            Vector3[] t_line_1 = new Vector3[2];
            t_line_1[0] = new Vector3();
            t_line_1[1] = TriangleMatrics.corners[i];
            lines.Add(t_line_1);

            Vector3[] t_line_2 = new Vector3[2];
            t_line_2[0] = TriangleMatrics.corners[i];
            t_line_2[1] = TriangleMatrics.corners[i + 1];
            lines.Add(t_line_2);
        }

        List<Vector3[]> t_lines = new List<Vector3[]>();

        if (linesHistory.Count < 1)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                t_lines.Add(lines[i]);
            }
        }
        else
        {
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < linesHistory.Count; j++)
                {
                    bool isSameLine = IsEqualLinePair(lines[i], linesHistory[j]);
                    //Debug.Log(isSameLine);
                    if (!isSameLine)
                    {
                        t_lines.Add(lines[i]);
                    }

                }
            }
        }

        GameObject edgesObject = new GameObject("CellEdges");
        edgesObject.transform.SetParent(cell.transform, false);
        edges = new CellEdge[t_lines.Count];

        for (int i = 0; i < t_lines.Count; i++)
        {
            CellEdge edge = edges[i] = Instantiate<CellEdge>(edgePrefab);
            edge.name = "Edge_" + i.ToString();
            edge.transform.SetParent(edgesObject.transform, false);

            //edge.transform.SetParent(cell.transform, false);
            //edge.transform.localPosition = cell.transform.position;


            LineRenderer t_lineRenderer = edges[i].gameObject.AddComponent<LineRenderer>();

            t_lineRenderer.material = material;

            t_lineRenderer.startColor = startColor;//设置颜色
            t_lineRenderer.endColor = endColor;

            t_lineRenderer.startWidth = width;//设置线的宽度
            t_lineRenderer.endWidth = width;

            t_lineRenderer.numCapVertices = numVertices;//设置端点圆滑度
            t_lineRenderer.numCornerVertices = numVertices;//设置拐角圆滑度，顶点越多越圆滑
            t_lineRenderer.useWorldSpace = false;


            Vector3[] t_line = t_lines[i];
            t_lineRenderer.positionCount = t_line.Length;

            for (int j = 0; j < t_line.Length; j++)
            {
                t_lineRenderer.SetPosition(j, t_line[j]);
            }
        }

    }


    bool IsEqualLinePair(Vector3[] newLine, Vector3[] oldLine)
    {
        bool flag = false;

        if (newLine[0] == oldLine[0])
        {
            if (newLine[1] == oldLine[1])
            {
                flag = true;
                return flag;
            }
        }

        if (newLine[0] == oldLine[1])
        {
            if (newLine[1] == oldLine[0])
            {
                flag = true;
                return flag;
            }
        }

        return flag;
    }
}
