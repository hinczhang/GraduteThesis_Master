using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEdge
{
    public static int Edge_Normal = 0;
    public static int Edge_Highlight = 1;
    public Vector3 start = new Vector3();
    public Vector3 end = new Vector3();
    public int type = Edge_Normal;
    public int edgeCount = 2;
    public int cellID = 0;
    public Vector3 center = new Vector3();
}
