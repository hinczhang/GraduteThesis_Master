using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMesh_Large : MonoBehaviour
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


    public void Draw(HexCell[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Draw(cells[i]);
        }
        linesHistory.Clear();
    }

    void Draw(HexCell cell)
    {

        Vector3 center = cell.transform.localPosition;

        lines.Clear();

        for (int i = 0; i < HexMatrics.corners.Length - 1; i++)
        {

            Vector3[] t_line = new Vector3[2];
            t_line[0] = HexMatrics_Large.corners[i];
            t_line[1] = HexMatrics_Large.corners[i + 1];
            lines.Add(t_line);
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
            edge.name = cell.name + "_Edge_" + i.ToString();
            edge.transform.SetParent(edgesObject.transform, false);


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


            Vector3 start = new Vector3();
            Vector3 end = new Vector3();

            start = t_line[0];
            end = t_line[1];

            GameObject colliderObject = new GameObject(edge.name + "_Collider");
            GameObject colliderObjectAOI = new GameObject(edge.name + "_Collider_AOI");
            int layer = LayerMask.NameToLayer("EyeTracking");
            colliderObjectAOI.layer = layer;

            colliderObject.transform.SetParent(edges[i].transform, false);
            colliderObjectAOI.transform.SetParent(edges[i].transform, false);

            BoxCollider col = colliderObject.AddComponent<BoxCollider>();
            BoxCollider colAOI = colliderObjectAOI.AddComponent<BoxCollider>();

            float lineLength = Vector3.Distance(start, end); // length of line
            col.size = new Vector3(lineLength, 0.02f, 0.02f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
            colAOI.size = new Vector3(lineLength, 0.11f, 0.11f);

            Vector3 midPoint = (start + end) / 2;
            col.transform.localPosition = midPoint; // setting position of collider object
                                                    // Following lines calculate the angle between startPos and endPos
            colAOI.transform.localPosition = midPoint;


            float angle = (Mathf.Abs(start.z - end.z) / Mathf.Abs(start.x - end.x));

            if ((start.z < end.z && start.x > end.x) || (end.z < start.z && end.x > start.x))
            {
                angle *= -1;
            }
            angle = -Mathf.Atan(angle) * 180 / Mathf.PI;

            col.transform.Rotate(0.0f, angle, 0.0f, Space.Self);
            colAOI.transform.Rotate(0.0f, angle, 0.0f, Space.Self);
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
