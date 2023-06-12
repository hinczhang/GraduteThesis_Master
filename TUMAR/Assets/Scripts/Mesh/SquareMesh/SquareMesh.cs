using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SquareMesh : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    public float lineWidth = 0.02f;
    public int numVertices = 2;
    public Material material = null;

    public float labelSize = 1.0f;

    public CellEdge edgePrefab;
    CellEdge[] edges;
    List<MeshEdge> linesDuplicated;
    List<MeshEdge> lines;

    int meshWidth = 0;
    int meshHeight = 0;

    int cardinal_size = 3;

    int xMax = 9999;
    int xMin = 10;
    int yMax = 48;
    int yMin = 7;

    void Awake()
    {
        lines = new List<MeshEdge>();
        linesDuplicated = new List<MeshEdge>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw(SquareCell[] cells, int width, int height)
    {
        meshWidth = width;
        meshHeight = height;
        //for (int i = 0; i < cells.Length; i++)
        //{
        //    Draw(cells[i]);
        //}

        GenerateEdges(cells);
        RenderEdges(cells);
        Label(cells);
        lines.Clear();
    }
    bool IsOmitted(SquareCell cell, int xMax, int xMin, int yMax, int yMin)
    {
        bool flag = false;
        int x = cell.coordinate.x;
        int y = cell.coordinate.y;
        if (x > xMin && x < xMax && y > yMin && y < yMax)
        {
            flag = true;
        }
        return flag;
    }

    void GenerateEdges(SquareCell[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            //if (IsOmitted(cells[i], xMax, xMin, yMax, yMin))
            //{
            //    continue;
            //}

            GenerateEdges(cells[i]);
        }
        RemoveDuplicateEdges();
    }

    void GenerateEdges(SquareCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        List<MeshEdge> t_lines = new List<MeshEdge>();
        for (int i = 0; i < SquareMatrics.corners.Length - 1; i++)
        {
            MeshEdge t_line = new MeshEdge();
            t_line.start = SquareMatrics.corners[i];
            t_line.end = SquareMatrics.corners[i + 1];
            t_line.cellID = cell.cellID;
            t_line.center = center;
            t_lines.Add(t_line);
        }


        if (cell.coordinate.x % cardinal_size == 0)
        {
            t_lines[0].type = MeshEdge.Edge_Highlight;
        }
        else if ((cell.coordinate.x + 1) % cardinal_size == 0)
        {
            t_lines[2].type = MeshEdge.Edge_Highlight;
        }

        if (cell.coordinate.y % cardinal_size == 0)
        {
            t_lines[3].type = MeshEdge.Edge_Highlight;
        }
        else if ((cell.coordinate.y + 1) % cardinal_size == 0)
        {
            t_lines[1].type = MeshEdge.Edge_Highlight;
        }

        for (int i = 0; i < t_lines.Count; i++)
        {
            linesDuplicated.Add(t_lines[i]);
        }
    }

    void RemoveDuplicateEdges()
    {
        for (int i = 0; i < linesDuplicated.Count; i++)
        {
            MeshEdge t_lineDuplicated = linesDuplicated[i];
            MeshEdge t_lineWithCenterDuplicated = new MeshEdge();
            t_lineWithCenterDuplicated.start = t_lineDuplicated.start + t_lineDuplicated.center;
            t_lineWithCenterDuplicated.end = t_lineDuplicated.end + t_lineDuplicated.center;

            bool isSameLine2 = false;
            for (int j = 0; j < lines.Count; j++)
            {
                MeshEdge t_line = lines[j];
                MeshEdge t_lineWithCenter = new MeshEdge(); ;
                t_lineWithCenter.start = t_line.start + t_line.center;
                t_lineWithCenter.end = t_line.end + t_line.center;
                isSameLine2 = IsEqualLinePair(t_lineWithCenterDuplicated, t_lineWithCenter);
                if (isSameLine2)
                {
                    break;
                }
            }

            if (!isSameLine2)
            {
                lines.Add(t_lineDuplicated);
            }
            else
            {
                continue;
            }
        }
    }


    void RenderEdges(SquareCell[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            //if (IsOmitted(cells[i], xMax, xMin, yMax, yMin))
            //{
            //    continue;
            //}

            RenderEdge(cells[i]);
        }
    }
    void RenderEdge(SquareCell cell)
    {
        GameObject edgesObject = new GameObject("CellEdges");
        edgesObject.transform.SetParent(cell.transform, false);
        edges = new CellEdge[lines.Count];

        for (int i = 0, j = 0; i < lines.Count; i++)
        {
            if (lines[i].cellID != cell.cellID)
            {
                continue;
            }

            MeshEdge t_line = lines[i];
            CellEdge edge = edges[i] = Instantiate<CellEdge>(edgePrefab);
            edge.name = cell.name + "_Edge_" + j.ToString();
            edge.transform.SetParent(edgesObject.transform, false);

            LineRenderer t_lineRenderer = edges[i].gameObject.AddComponent<LineRenderer>();
            t_lineRenderer.useWorldSpace = false;
            t_lineRenderer.positionCount = t_line.edgeCount;

            if (t_line.type == MeshEdge.Edge_Normal)
            {
                t_lineRenderer.material = material;

                t_lineRenderer.startColor = startColor;//设置颜色
                t_lineRenderer.endColor = endColor;

                t_lineRenderer.startWidth = lineWidth;//设置线的宽度
                t_lineRenderer.endWidth = lineWidth;

                t_lineRenderer.numCapVertices = numVertices;//设置端点圆滑度
                t_lineRenderer.numCornerVertices = numVertices;//设置拐角圆滑度，顶点越多越圆滑
            }
            else if (t_line.type == MeshEdge.Edge_Highlight)
            {
                t_lineRenderer.material = material;

                t_lineRenderer.startColor = Color.red;//设置颜色
                t_lineRenderer.endColor = Color.red;

                t_lineRenderer.startWidth = 0.02f;//设置线的宽度
                t_lineRenderer.endWidth = 0.02f;

                t_lineRenderer.numCapVertices = numVertices;//设置端点圆滑度
                t_lineRenderer.numCornerVertices = numVertices;//设置拐角圆滑度，顶点越多越圆滑
            }

            t_lineRenderer.SetPosition(0, t_line.start);
            t_lineRenderer.SetPosition(1, t_line.end);



            Vector3 start = new Vector3();
            Vector3 end = new Vector3();

            start = t_line.start;
            end = t_line.end;

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
            colAOI.size = new Vector3(lineLength, 0.1f, 0.1f);

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



            j++;
        }
    }


    void Label(SquareCell[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            //if (IsOmitted(cells[i], xMax, xMin, yMax, yMin))
            //{
            //    continue;
            //}
            //if (cells[i].coordinate.x % cardinal_size != 0 || cells[i].coordinate.y % cardinal_size != 0)
            //{
            //    continue;
            //}
            Label(cells[i]);
        }
    }

    void Label(SquareCell cell)
    {
        if (cell.coordinate.x % cardinal_size != 0 || cell.coordinate.y % cardinal_size != 0)
        {
            return;
        }

        int x = cell.coordinate.x - 6;
        int y = cell.coordinate.y - 3;

        //float r_text = basicR * level + 0.1f;
        GameObject textObject = new GameObject();
        textObject.name = "Label_" + x + "_" + y;

        Vector3 position_text = new Vector3();
        position_text.x = 0.0f;
        position_text.y = 0.1f;
        position_text.z = 0.0f;

        textObject.transform.SetParent(cell.transform, false);
        textObject.transform.localPosition = position_text;

        TextMeshPro t_text = textObject.gameObject.AddComponent<TextMeshPro>();
        t_text.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.3f);

        t_text.fontSize = labelSize;

        t_text.text = "(" + x + ", " + y + ")";

        t_text.alignment = TextAlignmentOptions.Center;

        BoxCollider boxCollider = textObject.gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(0.5f, 0.3f, 0.02f);

        FaceToCamera ftc = textObject.gameObject.AddComponent<FaceToCamera>();
        ftc.hideDist = 15;
        textObject.gameObject.AddComponent<HideDistant>();


    }

    void Draw(SquareCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        lines.Clear();
        List<Vector3[]> t_lines = new List<Vector3[]>();
        List<Vector3[]> t_lines_sub = new List<Vector3[]>();

        for (int i = 0; i < SquareMatrics.corners.Length - 1; i++)
        {
            MeshEdge t_line = new MeshEdge();
            t_line.start = SquareMatrics.corners[i];
            t_line.end = SquareMatrics.corners[i + 1];
            lines.Add(t_line);
        }

        //Vector3[] t_line1 = new Vector3[2];
        //Vector3[] t_line2 = new Vector3[2];
        //t_line1[0] = center + SquareMatrics.corners[0];
        //t_line1[1] = center + SquareMatrics.corners[2];
        //t_line2[0] = center + SquareMatrics.corners[1];
        //t_line2[1] = center + SquareMatrics.corners[3];
        //lines.Add(t_line1);
        //lines.Add(t_line2);

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

            t_lineRenderer.startWidth = lineWidth;//设置线的宽度
            t_lineRenderer.endWidth = lineWidth;

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

        if (t_lines_sub.Count > 0)
        {
            for (int i = 0; i < t_lines_sub.Count; i++)
            {
                CellEdge edge = edges[i] = Instantiate<CellEdge>(edgePrefab);
                edge.name = "Edge_Sub_" + i.ToString();
                edge.transform.SetParent(edgesObject.transform, false);


                LineRenderer t_lineRenderer = edges[i].gameObject.AddComponent<LineRenderer>();

                t_lineRenderer.material = material;

                t_lineRenderer.startColor = Color.red; ;//设置颜色
                t_lineRenderer.endColor = Color.red; ;

                t_lineRenderer.startWidth = 0.02f;//设置线的宽度
                t_lineRenderer.endWidth = 0.02f;

                t_lineRenderer.numCapVertices = numVertices;//设置端点圆滑度
                t_lineRenderer.numCornerVertices = numVertices;//设置拐角圆滑度，顶点越多越圆滑
                t_lineRenderer.useWorldSpace = false;


                Vector3[] t_line_sub = t_lines_sub[i];
                t_lineRenderer.positionCount = t_line_sub.Length;

                for (int j = 0; j < t_line_sub.Length; j++)
                {
                    t_lineRenderer.SetPosition(j, t_line_sub[j]);
                }
            }
        }
    }

    bool IsEqualLinePair(MeshEdge newLine, MeshEdge oldLine)
    {
        bool flag = false;

        if (newLine.start == oldLine.start)
        {
            if (newLine.end == oldLine.end)
            {
                flag = true;
                return flag;
            }
        }

        if (newLine.start == oldLine.end)
        {
            if (newLine.end == oldLine.start)
            {
                flag = true;
                return flag;
            }
        }

        return flag;
    }
}
