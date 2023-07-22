using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkBorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float width = 0.025f;
        SetWidth(width);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetWidth(float width)
    {       
        
        foreach (Transform childTf in transform)
        {
            Vector3 scale = childTf.localScale;
            scale.y = width;
            scale.z = width;
            childTf.localScale = scale;
        }
    }

 }
