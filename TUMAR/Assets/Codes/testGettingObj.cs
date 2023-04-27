using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;

public class testGettingObj : MonoBehaviour, IMixedRealityPointerHandler
{
    // private GameObject obj;
    List<VirtualLandmark> landmarks = new List<VirtualLandmark>();

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

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // 按下目光时的处理
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // 点击目光时的处理
        if (eventData.MixedRealityInputAction == MixedRealityInputAction.None)
        {
            // 没有指定任何输入操作
            return;
        }

        // 获取当前目光指向的物体
        GameObject focusedObject = eventData.Pointer.Result.CurrentPointerTarget;
        if (focusedObject == null)
        {
            return;
        } else {
            MeshRenderer renderer;
            if (focusedObject.TryGetComponent<MeshRenderer>(out renderer)) {
                // 访问renderer组件
                renderer = focusedObject.GetComponent<MeshRenderer>();
                renderer.material.color = Color.green;
                
            } else {
                // 没有renderer组件
                renderer = focusedObject.AddComponent<MeshRenderer>();
                renderer.material.color = Color.green;
            }   
        }
        // 对目标进行处理
        // ...
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // 松开目光时的处理
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        
    }
}
