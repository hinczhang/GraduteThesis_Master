using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;

public class testGettingObj : MonoBehaviour, IMixedRealityFocusHandler
{
    // private GameObject obj;
    List<VirtualLandmark> landmarks = new List<VirtualLandmark>();
    Dictionary<string, VirtualLandmark> landmarksDict = new Dictionary<string, VirtualLandmark>();
    HashSet<string> objectNames = new HashSet<string>();
    Camera mainCamera;

    void Awake()
    {
        // Get main camera
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Debug.Log("Main camera found: " + mainCamera.name);
            Vector3 cameraPosition = mainCamera.transform.position;
            Debug.Log("Camera position: " + cameraPosition);
        }
        else
        {
            Debug.Log("Main camera not found!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        

        // Get all landmarks
        string scenePath = Application.dataPath + "/Codes/landmarks.json";
        string sceneJson = File.ReadAllText(scenePath);
        ArrayData sceneData = JsonUtility.FromJson<ArrayData>(sceneJson);
        foreach (var item in sceneData.objs) {
            GameObject obj = GameObject.Find(item.name);
            if(obj == null) {
                Debug.Log("Object not found: " + item.id);
                continue;
            }
            VirtualLandmark landmark = new VirtualLandmark();
            landmark.SetLandmarkId(item.id);
            landmark.SetLandmarkName(item.name);
            landmark.SetLandmarkDescription(item.description);
            landmark.SetLandmarkRealName(item.realName);
            landmark.SetLandmarkType(item.type);
            
            landmark.SetGameObject(ref obj);
            landmarks.Add(landmark);
            landmarksDict.Add(item.name, landmark);
            objectNames.Add(item.name);
        }
        foreach (var item in landmarks) {
            GameObject obj = item.GetGameObject();
            setColor(ref obj, item.GetLandmarkType(), Color.white);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Camera positioning test.
        *    if(mainCamera != null) {
        *        Vector3 cameraPosition = mainCamera.transform.position;
        *        Debug.Log("Camera position: " + cameraPosition);
        *    }
        */
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        // Get the object being focused on
        GameObject focusedObject = eventData.NewFocusedObject;

        // Change color if the object is found
        if (focusedObject != null && objectNames.Contains(focusedObject.name))
        {
            VirtualLandmarkType type = landmarksDict[focusedObject.name].GetLandmarkType();
            setColor(ref focusedObject, type, Color.green);
        }
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        // Get the object being focused on
        GameObject focusedObject = eventData.OldFocusedObject;

        // Change color if the object is found
        if (focusedObject != null && objectNames.Contains(focusedObject.name))
        {
            VirtualLandmarkType type = landmarksDict[focusedObject.name].GetLandmarkType();
            setColor(ref focusedObject, type, Color.white);
        }
        
    }

    private void setColor(ref GameObject obj, VirtualLandmarkType type, Color color) {
        MeshRenderer renderer;
        if (obj.TryGetComponent<MeshRenderer>(out renderer)) {
            // Visit the renderer component
            renderer = obj.GetComponent<MeshRenderer>();
            if(color == Color.white) {
                if (type == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (type == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            } else {
                renderer.material.color = color;
            }
            
        } else {
            // No renderer component found, add one
            renderer = obj.AddComponent<MeshRenderer>();
            if(color == Color.white) {
                if (type == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (type == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            } else {
                renderer.material.color = color;
            }
        }
    }
}
