using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGettingObj : MonoBehaviour
{
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Experiment"); //替换为你的Cube对象的名称
        if (obj != null)
        {
            Vector3 size = GetObjectSize(obj);
            Debug.Log("Object size: " + size);
        } else {
            Debug.Log("Object not found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetObjectSize(GameObject obj)
    {
        Vector3 objectSize = Vector3.zero;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            objectSize = renderer.bounds.size;
        }
        return objectSize;
    }
}
