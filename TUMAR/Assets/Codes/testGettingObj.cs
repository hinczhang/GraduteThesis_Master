using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.UI;

public class testGettingObj : MonoBehaviour, IMixedRealityFocusHandler
{
    // private GameObject obj;
    private List<VirtualLandmark> landmarks = new List<VirtualLandmark>();
    public static List<ChunkLandmark> chunks = new List<ChunkLandmark>();
    private Dictionary<string, VirtualLandmark> landmarksDict = new Dictionary<string, VirtualLandmark>();
    public static Dictionary<int, VirtualLandmark> landmarksIdDict = new Dictionary<int, VirtualLandmark>();
    public static Dictionary<string, GameObject> spheres = new Dictionary<string, GameObject>();
    private HashSet<string> objectNames = new HashSet<string>();
    private Camera mainCamera;
    private Camera miniCamera;
    private int cameraHeight = 30;
    private GameObject Environment;
    private GameObject loc;
    private int minimapLayer;


    void Awake()
    {
        Environment = GameObject.Find("Environment");
        minimapLayer = LayerMask.NameToLayer("MinimapObjects");
        loc = GameObject.Find("Localization");
        // Get main camera
        mainCamera = Camera.main;
        miniCamera = GameObject.Find("miniCamera").GetComponent<Camera>();
        
        if (mainCamera != null)
        {
            miniCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraHeight, mainCamera.transform.position.z);
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

            Vector3 spherePosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 5, obj.transform.position.z);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = obj.name + "_sphere";
            sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sphere.transform.position = spherePosition;
            sphere.layer = minimapLayer;
            // sphere.transform.SetParent(Environment.transform, false);
            
            switch(item.type) {
                case 0: setColor(ref sphere, VirtualLandmarkType.PRIMARY, Color.white); break;
                case 1: setColor(ref sphere, VirtualLandmarkType.SECONDARY, Color.white); break;
                case 2: setColor(ref sphere, VirtualLandmarkType.NOT_SHOW, Color.white); break;
                default: setColor(ref sphere, VirtualLandmarkType.NOT_SHOW, Color.white); break;
            }
            sphere.SetActive(false);
            spheres.Add(item.name, sphere);
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
            ChunkLandmark chunkObj = GameObject.Find(chunk.name).AddComponent<ChunkLandmark>();
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
        miniCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraHeight, mainCamera.transform.position.z);
        loc.transform.position = new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z);
        loc.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
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
            setText(ref focusedObject, landmarksDict[focusedObject.name].GetLandmarkDescription());
            if(type == VirtualLandmarkType.PRIMARY) {
                foreach (var item in landmarksDict[focusedObject.name].GetConnectedLandmarks()) {
                    GameObject obj = item.GetGameObject();
                    obj.SetActive(true);
                    spheres[obj.name].SetActive(true);
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
            DestroyImmediate(GameObject.Find("Text_Notation"));
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

    private void setText(ref GameObject obj, string description) {
        // 创建一个空对象
        GameObject textObject = new GameObject("Text_Notation");

        // 将该对象添加到 cube 上
        Camera mainCamera = Camera.main;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 textPosition = obj.transform.position - cameraForward * 0.05f;
        textObject.transform.position = textPosition;
        textObject.transform.forward = obj.transform.forward;

        // 添加 TextMesh 组件
        TextMesh textMesh = textObject.AddComponent<TextMesh>();

        // 设置 TextMesh 的属性
        textMesh.text = "test";
        textMesh.fontSize = 36;
        textMesh.characterSize = 0.02f;
        textMesh.color = Color.blue;
        textMesh.alignment = TextAlignment.Center;
    }
}
