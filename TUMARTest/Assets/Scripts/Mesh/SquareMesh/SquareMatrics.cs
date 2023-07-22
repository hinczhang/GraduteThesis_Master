using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMatrics : MonoBehaviour
{
    public const float length = 1.0f;

    public static Vector3[] corners = {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, length),
        new Vector3(length, 0.0f, length),
        new Vector3(length, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 0.0f)
    };


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
