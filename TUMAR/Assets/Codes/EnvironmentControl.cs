using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.UI;

public class EnvironmentControl : MonoBehaviour, IMixedRealityFocusHandler
{
    // private GameObject obj;
    private List<VirtualLandmark> landmarks = new List<VirtualLandmark>();
    // public static List<ChunkLandmark> chunks = new List<ChunkLandmark>();
    private Dictionary<string, VirtualLandmark> landmarksDict = new Dictionary<string, VirtualLandmark>();
    public static Dictionary<int, VirtualLandmark> landmarksIdDict = new Dictionary<int, VirtualLandmark>();
    // public static Dictionary<string, GameObject> spheres = new Dictionary<string, GameObject>();
    private HashSet<string> objectNames = new HashSet<string>();
    private Camera mainCamera;
    // private Camera miniCamera;
    private int cameraHeight = 30;
    // private GameObject Environment;
    // private GameObject loc;
    // private int minimapLayer;

    void Awake()
    {
        // Environment = GameObject.Find("Environment");
        // minimapLayer = LayerMask.NameToLayer("MinimapObjects");
        // loc = GameObject.Find("Localization");
        // Get main camera
        mainCamera = Camera.main;
        // miniCamera = GameObject.Find("miniCamera").GetComponent<Camera>();
        
        if (mainCamera != null)
        {
            // miniCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraHeight, mainCamera.transform.position.z);
        }
        else
        {
            Debug.Log("Main camera not found!");
        }
    
        LandmarkData data = new LandmarkData();
        // Get all landmarks
        // string scenePath = Application.dataPath + "/Codes/landmarks.json";
        // string sceneJson = File.ReadAllText(scenePath);
        // ArrayData sceneData = JsonUtility.FromJson<ArrayData>(sceneJson);
        foreach (var item in data.objs) {
            GameObject obj = GameObject.Find(item.name);
            if(obj == null) {
                Debug.Log("Object not found: " + item.id);
                continue;
            }
            VirtualLandmark landmark = new VirtualLandmark();
            landmark.SetLandmarkId(item.id);
            // Debug.Log("Landmark id: " + item.id);
            landmark.SetLandmarkName(item.name);
            landmark.SetLandmarkDescription(item.description);
            landmark.SetLandmarkRealName(item.realName);
            landmark.SetLandmarkType(item.type);

            // Vector3 spherePosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 5, obj.transform.position.z);
            // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // sphere.name = obj.name + "_sphere";
            // sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // sphere.transform.position = spherePosition;
            // sphere.layer = minimapLayer;
            // sphere.transform.SetParent(Environment.transform, false);
            
            if(item.type == 0) {
                if(item.color.HasValue) {
                    Color tmp_color = item.color.Value;
                    if(!item.name.Contains("Door")) {
                        tmp_color = fadingColor(tmp_color, 0.75f);
                    } else {
                        planeToFrame(ref obj);
                        for(int i = 1; i <= 8; ++i) {
                            GameObject cylinder = obj.transform.Find($"{i}").gameObject;
                            if(cylinder != null) {
                                cylinder.GetComponent<Renderer>().material.color = tmp_color;
                            }
                        }
                    }
                    landmark.SetLandmarkColor(tmp_color);
                    if(item.name.Contains("Door")) {
                        setColor(ref obj, VirtualLandmarkType.PRIMARY, ColorUtility.TRANSPARENT);
                    } else {
                        setColor(ref obj, VirtualLandmarkType.PRIMARY, landmark.GetLandmarkColor());
                    }
                    
                }
                
                // setColor(ref sphere, VirtualLandmarkType.PRIMARY, landmark.GetLandmarkColor());
            }
            if(item.type == 2) {
                obj.SetActive(false);
            }
            // sphere.SetActive(false);
            // spheres.Add(item.name, sphere);
            /* if(item.type != 0) {
                obj.SetActive(false);
            }*/ 
            // obj.SetActive(false);
            landmark.SetGameObject(ref obj);
            landmarks.Add(landmark);
            landmarksDict.Add(item.name, landmark);
            landmarksIdDict.Add(item.id, landmark);
            objectNames.Add(item.name);
        }

        foreach (var item in data.objs) {
            if(item.type == 0) {
                Color color = landmarksDict[item.name].GetLandmarkColor();
                for(int i = 0; i < item.connectedIDs.Count; i++) {
                    landmarksDict[item.name].AddConnectedLandmark(landmarksIdDict[item.connectedIDs[i]]);
                    // make the color less bright if not the important landmark
                    Color fadedColor;
                    if(landmarksIdDict[item.connectedIDs[i]].GetLandmarkType() == VirtualLandmarkType.PRIMARY) {
                        fadedColor = landmarksIdDict[item.connectedIDs[i]].GetLandmarkColor();
                    } else {
                        fadedColor = fadingColor(color, 0.75f);
                        landmarksIdDict[item.connectedIDs[i]].SetLandmarkColor(fadedColor);
                    }
                    GameObject obj = landmarksIdDict[item.connectedIDs[i]].GetGameObject();
                    if(obj.name.Contains("Door")) {
                        planeToFrame(ref obj);
                        for(int id = 1; id <= 8; ++id) {
                            GameObject cylinder = obj.transform.Find($"{id}").gameObject;
                            if(cylinder != null) {
                                cylinder.GetComponent<Renderer>().material.color = fadedColor;
                            }
                        }
                    }

                    if(item.name.Contains("Door")) {
                        setColor(ref obj, VirtualLandmarkType.PRIMARY, ColorUtility.TRANSPARENT);
                    } else {
                        setColor(ref obj, VirtualLandmarkType.PRIMARY, fadedColor);
                    }
                }
            }
        }
        // foreach (var item in landmarks) {
        //    GameObject obj = item.GetGameObject();
        //    setColor(ref obj, item.GetLandmarkType(), Color.white);
        // }
        
        // Get chunks
        /*
        string chunkPath = null;
        #if UNITY_EDITOR
            chunkPath = Application.dataPath + "/Codes/chunks.json";
        #else
            chunkPath = Application.persistentDataPath + "/Codes/chunks.json";
        #endif
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
        */
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        // miniCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraHeight, mainCamera.transform.position.z);
        // loc.transform.position = new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z);
        // loc.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);

    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        // Get the object being focused on
        GameObject focusedObject = eventData.NewFocusedObject;

        // Change color if the object is found
        if (focusedObject != null && objectNames.Contains(focusedObject.name))
        {
            VirtualLandmarkType type = landmarksDict[focusedObject.name].GetLandmarkType();
            Color color = landmarksDict[focusedObject.name].GetLandmarkColor();
            if(focusedObject.name.Contains("Door")) {
                for(int id = 1; id <= 8; ++id) {
                    GameObject cylinder = focusedObject.transform.Find($"{id}").gameObject;
                    if(cylinder != null) {
                        cylinder.GetComponent<Renderer>().material.color = ColorUtility.HIGHLIGHT;
                    }
                }
            } else {
                setColor(ref focusedObject, type, ColorUtility.HIGHLIGHT);
            }
            // setColor(ref focusedObject, type, ColorUtility.HIGHLIGHT);
            setText(ref focusedObject, landmarksDict[focusedObject.name].GetLandmarkDescription());
            foreach (var item in landmarksDict[focusedObject.name].GetConnectedLandmarks()) {
                GameObject obj = item.GetGameObject();
                // setColor(ref obj, item.GetLandmarkType(), ColorUtility.HIGHLIGHT);
                if(obj.name.Contains("Door")) {
                    for(int id = 1; id <= 8; ++id) {
                        GameObject cylinder = obj.transform.Find($"{id}").gameObject;
                        if(cylinder != null) {
                            cylinder.GetComponent<Renderer>().material.color = ColorUtility.HIGHLIGHT;
                        }
                    }
                } else {
                    setColor(ref obj, type, ColorUtility.HIGHLIGHT);
                }
            }
            /*if(type == VirtualLandmarkType.PRIMARY) {
                foreach (var item in landmarksDict[focusedObject.name].GetConnectedLandmarks()) {
                    GameObject obj = item.GetGameObject();
                    item.SetLandmarkColor(color);
                    setColor(ref obj, item.GetLandmarkType(), color);
                    obj.SetActive(true);
                    // spheres[obj.name].SetActive(true);
                    // setColor(ref obj, item.GetLandmarkType(), Color.green);
                }
            }*/
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
            //setColor(ref focusedObject, type, landmarksDict[focusedObject.name].GetLandmarkColor());
            if(focusedObject.name.Contains("Door")) {
                for(int id = 1; id <= 8; ++id) {
                    GameObject cylinder = focusedObject.transform.Find($"{id}").gameObject;
                    if(cylinder != null) {
                        cylinder.GetComponent<Renderer>().material.color = landmarksDict[focusedObject.name].GetLandmarkColor();
                    }
                }
            } else {
                setColor(ref focusedObject, type, landmarksDict[focusedObject.name].GetLandmarkColor());
            }
            foreach (var item in landmarksDict[focusedObject.name].GetConnectedLandmarks()) {
                GameObject obj = item.GetGameObject();
                //setColor(ref obj, item.GetLandmarkType(), item.GetLandmarkColor());
                if(obj.name.Contains("Door")) {
                    for(int id = 1; id <= 8; ++id) {
                        GameObject cylinder = obj.transform.Find($"{id}").gameObject;
                        if(cylinder != null) {
                            cylinder.GetComponent<Renderer>().material.color = item.GetLandmarkColor();
                        }
                    }
                } else {
                    setColor(ref obj, type, item.GetLandmarkColor());
                }
            }
            DestroyImmediate(GameObject.Find("Text_Notation"));
        }
        
    }

    private void setColor(ref GameObject obj, VirtualLandmarkType type, Color color) {
        MeshRenderer renderer;
        if (obj.TryGetComponent<MeshRenderer>(out renderer)) {
            // Visit the renderer component
            renderer = obj.GetComponent<MeshRenderer>();
            /*if(color == Color.white) {
                if (type == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (type == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            } else {*/
            // renderer.material.color = color;
            // }
            
        } else {
            // No renderer component found, add one
            renderer = obj.AddComponent<MeshRenderer>();
            /*if(color == Color.white) {
                if (type == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (type == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            } else {*/
            // renderer.material.color = color;
            // }
        }
        Material[] materials = renderer.materials;
        foreach (var material in materials) {
            material.color = color;
        }
    }

    private void setText(ref GameObject obj, string description) {
        // 创建一个空对象
        GameObject textObject = new GameObject("Text_Notation");

        // 将该对象添加到 cube 上
        Camera mainCamera = Camera.main;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 textPosition = obj.transform.position - cameraForward * 1f;
        textObject.transform.position = textPosition;
        textObject.transform.forward = mainCamera.transform.forward;

        // 添加 TextMesh 组件
        TextMesh textMesh = textObject.AddComponent<TextMesh>();

        // 设置 TextMesh 的属性
        textMesh.text = description;
        textMesh.fontSize = 90;
        textMesh.characterSize = 0.02f;
        textMesh.color = Color.white;
        textMesh.alignment = TextAlignment.Center;

        /* TextMesh outlineTextMesh = Instantiate(textMesh, textObject.transform);
        TextMesh mainTextMesh = Instantiate(textMesh, textObject.transform);
        outlineTextMesh.transform.localPosition = Vector3.zero; // 重置边缘TextMesh的本地位置
        outlineTextMesh.color = Color.black; // 设置边缘颜色
        outlineTextMesh.GetComponent<Renderer>().sortingOrder = textMesh.GetComponent<Renderer>().sortingOrder - 1; // 将边缘TextMesh的渲染层级设为原始TextMesh的前一层

        // 根据相机朝向调整边缘TextMesh的朝向
        outlineTextMesh.transform.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        mainTextMesh.transform.localPosition = Vector3.zero; // 重置主要TextMesh的本地位置
        mainTextMesh.color = Color.white; // 设置主要文字颜色*/
    }

    private Color fadingColor(Color color, float fadeAmount = 0.25f) {
        Color.RGBToHSV(color, out float initialHue, out float initialSaturation, out float initialValue);
        float fadedValue = Mathf.Clamp01(initialSaturation * fadeAmount);
        Color fadedColor = Color.HSVToRGB(initialHue, fadedValue, initialValue);
        fadedColor.a = 0.5f;
        return fadedColor;
    }

    private void planeToFrame(ref GameObject obj) {
        // 获取 Plane 对象的变换组件
        Transform planeTransform = obj.transform;
        Vector3 position = planeTransform.position;
        Vector3 n = planeTransform.TransformDirection(Vector3.up);
        

        float h = planeTransform.localScale.x*10;
        float w = planeTransform.localScale.z*10;

        // 计算四个顶点的坐标
        Vector3 topLeftVertex = position + new Vector3(0, h/2, 0);
        Vector3 topRightVertex = position + new Vector3(0, h/2, 0);
        Vector3 bottomLeftVertex = position + new Vector3(0, -h/2, 0);
        Vector3 bottomRightVertex = position + new Vector3(0, -h/2, 0);
        if (n.z == 0.0f) {
            topLeftVertex.z -= w/2;
            topRightVertex.z += w/2;
            bottomLeftVertex.z -= w/2;
            bottomRightVertex.z += w/2;
        } else {
            topLeftVertex.x -= w/2;
            topRightVertex.x += w/2;
            bottomLeftVertex.x -= w/2;
            bottomRightVertex.x += w/2;
        }

        DrawCylinderBetweenPoints(topLeftVertex, topRightVertex, "1" ,ref planeTransform);
        DrawCylinderBetweenPoints(topRightVertex, bottomRightVertex, "2", ref planeTransform);
        DrawCylinderBetweenPoints(bottomRightVertex, bottomLeftVertex, "3", ref planeTransform);
        DrawCylinderBetweenPoints(bottomLeftVertex, topLeftVertex, "4", ref planeTransform);
        GenerateSphere(topLeftVertex, "5",ref planeTransform);
        GenerateSphere(topRightVertex, "6",ref planeTransform);
        GenerateSphere(bottomLeftVertex, "7", ref planeTransform);
        GenerateSphere(bottomRightVertex, "8", ref planeTransform);
    }

    private void DrawCylinderBetweenPoints(Vector3 pointA, Vector3 pointB, string name, ref Transform planeTransform , float radius = 0.005f) {
        GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        edge.name = name;
        float distance = Vector3.Distance(pointA, pointB);
        // 设置圆柱体位置为两个点的中点
        edge.transform.position = (pointA + pointB) / 2f;
        // 设置圆柱体的缩放，使其长度与两个点之间的距离匹配
        edge.transform.localScale = new Vector3(radius * 2f, distance / 2f, radius * 2f);
        // 计算圆柱体的方向向量
        Vector3 direction = pointB - pointA;
        // 设置圆柱体的旋转，使其朝向两个点之间的方向
        //edge.transform.rotation = Quaternion.LookRotation(direction);
        edge.transform.up = direction.normalized;
        // 获取圆柱体的渲染器组件
        Renderer renderer = edge.GetComponent<Renderer>();
        edge.transform.SetParent(planeTransform);
    }

    private void GenerateSphere(Vector3 position, string name, ref Transform planeTransform, float radius = 0.05f)
    {
        // 创建球体游戏对象
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        // 设置球体的缩放，使其半径与指定的半径匹配
        sphere.transform.localScale = new Vector3(radius * 2f, radius * 2f, radius * 2f);
        sphere.transform.position = position;
        sphere.transform.SetParent(planeTransform);
    }
    
}
