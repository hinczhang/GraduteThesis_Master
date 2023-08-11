using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{


    public Color color;

    public Vector3[] vertices;

    public int cellID;

    void Awake()
    {
        vertices = new Vector3[6];
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
