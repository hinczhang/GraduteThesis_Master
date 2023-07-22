using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWidth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float width = 0.05f;
        SetWidth(width); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetWidth(float width)
    {
        Vector3 scale = transform.localScale;
        scale.y = width;
        scale.z = width;
        transform.localScale = scale;
    }
}
