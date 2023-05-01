using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;

public class testGettingObj : MonoBehaviour, IMixedRealityFocusHandler
{
    // private GameObject obj;
    private List<VirtualLandmark> landmarks = new List<VirtualLandmark>();
    private List<ChunkLandmark> chunks = new List<ChunkLandmark>();
    private Dictionary<string, VirtualLandmark> landmarksDict = new Dictionary<string, VirtualLandmark>();
    private Dictionary<int, VirtualLandmark> landmarksIdDict = new Dictionary<int, VirtualLandmark>();
    private HashSet<string> objectNames = new HashSet<string>();
    private Camera mainCamera;

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
            /* if(item.type != 0) {
                obj.SetActive(false);
            }*/ 
            obj.SetActive(false);
            landmark.SetGameObject(ref obj);
            landmarks.Add(landmark);
            landmarksDict.Add(item.name, landmark);
            landmarksIdDict.Add(item.id, landmark);
            objectNames.Add(item.name);
        }

        foreach (var item in sceneData.objs) {
            if(item.type == 0) {
                for(int i = 0; i < item.connectedIDs.Count; i++) {
                    landmarksDict[item.name].AddConnectedLandmark(landmarksIdDict[item.connectedIDs[i]]);
                }
            }
        }
        foreach (var item in landmarks) {
            GameObject obj = item.GetGameObject();
            setColor(ref obj, item.GetLandmarkType(), Color.white);
        }
        
        // Get chunks
        string chunkPath = Application.dataPath + "/Codes/chunks.json";
        string chunkJson = File.ReadAllText(chunkPath);
        ArrayChunk chunkData = JsonUtility.FromJson<ArrayChunk>(chunkJson);
        foreach (var chunk in chunkData.chunks) {
            ChunkLandmark chunkObj = GameObject.Find(chunk.name).GetComponent<ChunkLandmark>();
            chunkObj.SetChunkName(chunk.name);
            foreach (var id in chunk.objs) {
                chunkObj.AddChunkObj(landmarksIdDict[id].GetGameObject());
            }
            chunks.Add(chunkObj);
        }
    }

    // Start is called before the first frame update
    void Start() {

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

            if(type == VirtualLandmarkType.PRIMARY) {
                foreach (var item in landmarksDict[focusedObject.name].GetConnectedLandmarks()) {
                    GameObject obj = item.GetGameObject();
                    obj.SetActive(true);
                    // setColor(ref obj, item.GetLandmarkType(), Color.green);
                }
            }
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
