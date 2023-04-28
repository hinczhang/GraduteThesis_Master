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
    // TODO: 将landmarks的List改为Dictionary，key为name，value为VirtualLandmark
    // TODO: 将各个object的name集成在HashSet<string>中，用于判断是否存在，因为我们只对研究对象object进行操作

    // Start is called before the first frame update
    void Start()
    {
        string scenePath = Application.dataPath + "/Codes/landmarks.json";
        string sceneJson = File.ReadAllText(scenePath);
        ArrayData sceneData = JsonUtility.FromJson<ArrayData>(sceneJson);
        Debug.Log(sceneData.objs.Count);
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
        }
        foreach (var item in landmarks) {
            GameObject obj = item.GetGameObject();
            MeshRenderer renderer;
            if (obj.TryGetComponent<MeshRenderer>(out renderer)) {
                // 访问renderer组件
                renderer = obj.GetComponent<MeshRenderer>();
                if (item.GetLandmarkType() == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (item.GetLandmarkType() == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            } else {
                // 没有renderer组件
                renderer = obj.AddComponent<MeshRenderer>();
                if (item.GetLandmarkType() == VirtualLandmarkType.PRIMARY) {
                    renderer.material.color = Color.red;
                } else if (item.GetLandmarkType() == VirtualLandmarkType.SECONDARY) {
                    renderer.material.color = Color.yellow;
                } else {
                    renderer.material.color = Color.blue;
                }
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        // 获取被注视的游戏对象
        GameObject focusedObject = eventData.NewFocusedObject;

        // 如果找到了匹配的Cube对象，则将其变色
        if (focusedObject != null)
        {
            Debug.Log("Focused Object: " + focusedObject.name);
        }
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        
    }
}
