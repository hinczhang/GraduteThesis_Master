using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CircleMesh : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    public float lineWidth = 0.01f;
    public int numVertices = 2;
    public Material material = null;

    public CellEdge edgePrefab;
    CellEdge[] edges;
    List<MeshEdge> lines;
    int cardinalLevel_size = 3;
    int cardinalSector_size = 3;

    public float labelSize = 1.0f;

    void Awake()
    {
        lines = new List<MeshEdge>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw(CircleCell[] cells, float basicR = 1.0f, int lineNum = 720, int sectorNum = 12)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Draw(cells[i], basicR, lineNum, sectorNum, i);
            //DrawOld(cells[i], basicR, lineNum, sectorNum, i);
        }
        Label(cells, basicR, sectorNum);
    }


    void Draw(CircleCell cell, float basicR, int lineNum, int sectorNum, int level)
    {
        lines.Clear();
        Vector3 center = cell.transform.localPosition;

        int arcNum = 0;
        int meshType = 0;
        if ((level + 1) % cardinalLevel_size == 0)
        {
            meshType = MeshEdge.Edge_Highlight;
        }

        for (int i = 0; i < lineNum; i++)
        {
            MeshEdge t_line = new MeshEdge();
            Vector3 offset1 = new Vector3();
            Vector3 offset2 = new Vector3();
            float radius = basicR * (level + 1);
            float radian1 = 2 * Mathf.PI * (float)i / (float)lineNum;
            float radian2 = 2 * Mathf.PI * (float)(i + 1) / (float)lineNum;

            offset1.x = Mathf.Cos(radian1) * radius;
            offset1.z = Mathf.Sin(radian1) * radius;


            offset2.x = Mathf.Cos(radian2) * radius;
            offset2.z = Mathf.Sin(radian2) * radius;

            if (offset1 != offset2)
            {
                t_line.start = offset1;
                t_line.end = offset2;
                t_line.type = meshType;
                t_line.cellID = cell.cellID;
                t_line.center = center;
                lines.Add(t_line);
            }
        }
        arcNum = lines.Count;

        for (int i = 0; i < sectorNum; i++)
        {
            MeshEdge t_line = new MeshEdge();
            Vector3 offset1 = new Vector3();
            Vector3 offset2 = new Vector3();

            float radiusSmall = basicR * level;
            float radiusLarge = basicR * (level + 1);

            float radian = 2 * Mathf.PI * (float)i / sectorNum;

            offset1.x = Mathf.Cos(radian) * radiusSmall;
            offset1.z = Mathf.Sin(radian) * radiusSmall;


            offset2.x = Mathf.Cos(radian) * radiusLarge;
            offset2.z = Mathf.Sin(radian) * radiusLarge;

            int sectorMeshType = 0;

            if (i % cardinalSector_size == 0)
            {
                sectorMeshType = MeshEdge.Edge_Highlight;
            }
            else
            {
                sectorMeshType = MeshEdge.Edge_Normal;
            }


            if (offset1 != offset2)
            {
                t_line.start = offset1;
                t_line.end = offset2;
                t_line.type = sectorMeshType;
                t_line.cellID = cell.cellID;
                t_line.center = center;
                lines.Add(t_line);
            }
        }


        GameObject edgesObject = new GameObject("CellEdges");
        edgesObject.transform.SetParent(cell.transform, false);
        edges = new CellEdge[lines.Count];

        for (int i = 0; i < lines.Count; i++)
        {
            MeshEdge t_line = lines[i];
            CellEdge edge = edges[i] = Instantiate<CellEdge>(edgePrefab);
            edge.name = cell.name + "_Edge_" + i.ToString();
            edge.transform.SetParent(edgesObject.transform, false);
            //edge.transform.localPosition = cell.transform.position;

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
        }



    }

    void Label(CircleCell[] cells, float basicR = 1.0f, int sectorNum = 12)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if ((i + 1) % cardinalLevel_size != 0)
            {
                continue;
            }
            Label(cells[i], basicR, sectorNum, i);
        }
    }

    void Label(CircleCell cell, float basicR, int sectorNum, int level)
    {
        for (int i = 0; i < sectorNum; i++)
        {
            if (i % cardinalSector_size != 0)
            {
                continue;
            }
            MeshEdge t_line = new MeshEdge();
            Vector3 offset = new Vector3();

            float radiusSmall = basicR * level;
            float radiusLarge = basicR * (level + 1);

            float radian = -2 * Mathf.PI * ((float)i / sectorNum - 0.25f);
            int degree = i * 360 / sectorNum;
            offset.x = Mathf.Cos(radian) * radiusLarge;
            offset.z = Mathf.Sin(radian) * radiusLarge;


            GameObject textObject = new GameObject();
            int showedLevel = level + 1;
            textObject.name = "Label_" + showedLevel + "_" + degree;

            Vector3 position_text = new Vector3();
            position_text.x = offset.x;
            position_text.y = 0.1f;
            position_text.z = offset.z;            


            textObject.transform.SetParent(cell.transform, false);
            textObject.transform.localPosition = position_text;

            float new_for_x = position_text.x;
            float new_for_y = textObject.transform.position.y;
            float new_for_z = position_text.z;
            textObject.transform.forward = new Vector3(new_for_x, new_for_y, new_for_z);

            TextMeshPro t_text = textObject.gameObject.AddComponent<TextMeshPro>();
            t_text.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.3f);

            t_text.fontSize = labelSize;
            t_text.text = showedLevel + ", " + degree + "°";

            t_text.alignment = TextAlignmentOptions.Center;

            BoxCollider boxCollider = textObject.gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(0.5f, 0.3f, 0.02f);

            FaceToCamera ftc = textObject.gameObject.AddComponent<FaceToCamera>();
            ftc.hideDist = 15;
            //textObject.gameObject.AddComponent<HideDistant>();
        }
    }

    //void DrawOld(CircleCell cell, float basicR, int lineNum, int sectorNum, int level)
    //{
    //    Vector3 center = cell.transform.localPosition;

    //    List<Vector3[]> t_lines = new List<Vector3[]>();

    //    int arcNum = 0;
    //    for (int i = 0; i < lineNum - 1; i++)
    //    {
    //        Vector3[] t_line = new Vector3[2];
    //        Vector3 offset1 = new Vector3();
    //        Vector3 offset2 = new Vector3();
    //        float radius = basicR * level;
    //        float radian1 = 2 * Mathf.PI * (float)i / (float)lineNum;
    //        float radian2 = 2 * Mathf.PI * (float)(i + 1) / (float)lineNum;

    //        offset1.x = Mathf.Cos(radian1) * radius;
    //        offset1.z = Mathf.Sin(radian1) * radius;


    //        offset2.x = Mathf.Cos(radian2) * radius;
    //        offset2.z = Mathf.Sin(radian2) * radius;

    //        if (offset1 != offset2)
    //        {
    //            t_line[0] = offset1;
    //            t_line[1] = offset2;
    //            t_lines.Add(t_line);
    //        }
    //    }
    //    arcNum = t_lines.Count;

    //    for (int i = 0; i < sectorNum; i++)
    //    {
    //        Vector3[] t_line = new Vector3[2];
    //        Vector3 offset1 = new Vector3();
    //        Vector3 offset2 = new Vector3();

    //        float radiusSmall = basicR * level;
    //        float radiusLarge = basicR * (level + 1);

    //        float radian = 2 * Mathf.PI * (float)i / sectorNum;

    //        offset1.x = Mathf.Cos(radian) * radiusSmall;
    //        offset1.z = Mathf.Sin(radian) * radiusSmall;


    //        offset2.x = Mathf.Cos(radian) * radiusLarge;
    //        offset2.z = Mathf.Sin(radian) * radiusLarge;

    //        if (offset1 != offset2)
    //        {
    //            t_line[0] = offset1;
    //            t_line[1] = offset2;
    //            t_lines.Add(t_line);
    //        }
    //    }

    //    GameObject edgesObject = new GameObject("CellEdges");
    //    edgesObject.transform.SetParent(cell.transform, false);
    //    edges = new CellEdge[t_lines.Count];



    //    for (int i = 0; i < t_lines.Count; i++)
    //    {
    //        CellEdge edge = edges[i] = Instantiate<CellEdge>(edgePrefab);
    //        edge.name = "Edge_" + i.ToString();
    //        edge.transform.SetParent(edgesObject.transform, false);
    //        //edge.transform.localPosition = cell.transform.position;


    //        LineRenderer t_lineRenderer = edges[i].gameObject.AddComponent<LineRenderer>();
    //        Color t_startColor = new Color();
    //        Color t_endColor = new Color();
    //        float t_width = 0.01f;
    //        if (level % 5 == 0 && level != 0 && i < arcNum)
    //        {
    //            t_width = 0.02f;
    //            t_startColor = Color.red;
    //            t_endColor = Color.red;
    //        }
    //        else
    //        {
    //            t_width = lineWidth;
    //            t_startColor = startColor;
    //            t_endColor = endColor;
    //        }
    //        //t_startColor = startColor;
    //        //t_endColor = endColor;

    //        t_lineRenderer.material = material;

    //        t_lineRenderer.startColor = t_startColor;//设置颜色
    //        t_lineRenderer.endColor = t_endColor;



    //        t_lineRenderer.startWidth = t_width;//设置线的宽度
    //        t_lineRenderer.endWidth = t_width;

    //        t_lineRenderer.numCapVertices = numVertices;//设置端点圆滑度
    //        t_lineRenderer.numCornerVertices = numVertices;//设置拐角圆滑度，顶点越多越圆滑
    //        t_lineRenderer.useWorldSpace = false;

    //        Vector3[] t_line = t_lines[i];
    //        t_lineRenderer.positionCount = t_line.Length;

    //        for (int j = 0; j < t_line.Length; j++)
    //        {
    //            t_lineRenderer.SetPosition(j, t_line[j]);
    //        }
    //    }

    //    //if (level % 5 == 0 && level != 0)
    //    //{
    //    //    float r_text = basicR * level + 0.1f;
    //    //    GameObject textObject = new GameObject("Annotation");

    //    //    TextMeshPro t_text = textObject.gameObject.AddComponent<TextMeshPro>();
    //    //    t_text.fontSize = 3;
    //    //    t_text.text = level.ToString();

    //    //    t_text.alignment = TextAlignmentOptions.Center;

    //    //    Vector3 position_text = new Vector3();
    //    //    position_text.x = 0.0f;
    //    //    position_text.y = -1.5f;
    //    //    position_text.z = r_text;

    //    //    textObject.transform.SetParent(cell.transform, false);
    //    //    textObject.transform.position = position_text;
    //    //}



    //}
}
