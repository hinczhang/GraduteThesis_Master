using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCell : MonoBehaviour
{
    public Vector3[] vertices;
    public Vector2Int coordinate;
    public int cellID = 0;
    void Awake()
    {
        vertices = new Vector3[4];
        coordinate = new Vector2Int();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
