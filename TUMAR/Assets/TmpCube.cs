using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VirtualLandmark virtualLandmark = new VirtualLandmark();
        virtualLandmark.SetLandmarkName("OKK");
        Debug.Log(gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
