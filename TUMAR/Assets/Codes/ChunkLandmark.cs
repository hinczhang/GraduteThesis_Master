using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkData
{
    public string name;
    public List<int> objs;
}

public class ArrayChunk
{
    public List<ChunkData> chunks;
}

public class ChunkLandmark : MonoBehaviour
{
    private string chunkName;
    private List<GameObject> chunkObjs = new List<GameObject>();
    private Camera mainCamera;
    private Collider m_Collider;
    private bool isActive = false;
    GameObject obj;
    void Awake()
    {
        
        // Get main camera
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            /* Debug.Log("Main camera found: " + mainCamera.name);
            Vector3 cameraPosition = mainCamera.transform.position;
            Debug.Log("Camera position: " + cameraPosition); */
        }
        else
        {
            Debug.Log("Main camera not found!");
        }

        obj = gameObject;
        if (obj.TryGetComponent<Collider>(out m_Collider)) {
            // Visit the collider component
            m_Collider = obj.GetComponent<Collider>();
        } else {
            // No collider component found, add one
            m_Collider = obj.AddComponent<Collider>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(!isActive) {
            if (m_Collider.bounds.Contains(mainCamera.transform.position)) {
                isActive = true;
                foreach (var subObj in chunkObjs) {
                    // string name = subObj.name;
                    // GameObject miniObj = EnvironmentControl.spheres[name];
                    
                    // miniObj.SetActive(true);
                    subObj.SetActive(true);
                }
                // miniMap.SendMessage("reDrawMap", chunkName);
            }
        }
        
    }

    // Set chunk name
    public void SetChunkName(string name)
    {
        chunkName = name;
        
    }

    // Get chunk name
    public string GetChunkName()
    {
        return chunkName;
    }

    // Set chunk obj
    public void AddChunkObj(GameObject obj)
    {
        chunkObjs.Add(obj);
    }

    // Get chunk obj
    public List<GameObject> GetChunkObjs()
    {
        return chunkObjs;
    }
}
