using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;


[Serializable]
public class InfoData
{
    public int id;
    public int type;
    public string name;
    public string realName;
    public string description;
}

[Serializable]
public class ArrayData
{
    public List<InfoData> objs;
}

public enum VirtualLandmarkType
{
    PRIMARY,
    SECONDARY,
    NOT_SHOW
};


public class VirtualLandmark : BaseEyeFocusHandler
{
    private int landmarkId;
    private string landmarkName;
    private string landmarkDescription;
    private string landmarkRealName;
    private VirtualLandmarkType landmarkType;
    private Dictionary<int, VirtualLandmark> connectedLandmarks;
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    protected override void OnEyeFocusStart()
    {
        MeshRenderer renderer;
        if (obj.TryGetComponent<MeshRenderer>(out renderer)) {
            // 访问renderer组件
            renderer = obj.GetComponent<MeshRenderer>();
            renderer.material.color = Color.green;
            
        } else {
            // 没有renderer组件
            renderer = obj.AddComponent<MeshRenderer>();
            renderer.material.color = Color.green;
        }    
    }

    protected override void OnEyeFocusStop()
    {
        MeshRenderer renderer;
        if (obj.TryGetComponent<MeshRenderer>(out renderer)) {
            // 访问renderer组件
            renderer = obj.GetComponent<MeshRenderer>();
            if (landmarkType == VirtualLandmarkType.PRIMARY) {
                renderer.material.color = Color.red;
            } else if (landmarkType == VirtualLandmarkType.SECONDARY) {
                renderer.material.color = Color.yellow;
            } else {
                renderer.material.color = Color.blue;
            }
        } else {
            // 没有renderer组件
            renderer = obj.AddComponent<MeshRenderer>();
            if (landmarkType == VirtualLandmarkType.PRIMARY) {
                renderer.material.color = Color.red;
            } else if (landmarkType == VirtualLandmarkType.SECONDARY) {
                renderer.material.color = Color.yellow;
            } else {
                renderer.material.color = Color.blue;
            }
        }    
    }
    */
    // Set landmark id
    public void SetLandmarkId(int id)
    {
        landmarkId = id;
    }

    // Get landmark id
    public int GetLandmarkId()
    {
        return landmarkId;
    }

    // Set landmark name
    public void SetLandmarkName(string name)
    {
        landmarkName = name;
        obj = GameObject.Find(landmarkName);
    }

    // Get landmark name
    public string GetLandmarkName()
    {
        return landmarkName;
    }

    // Set landmark description
    public void SetLandmarkDescription(string description)
    {
        landmarkDescription = description;
    }

    // Get landmark description
    public string GetLandmarkDescription()
    {
        return landmarkDescription;
    }

    // Set landmark real name
    public void SetLandmarkRealName(string name)
    {
        landmarkRealName = name;
    }

    // Get landmark real name
    public string GetLandmarkRealName()
    {
        return landmarkRealName;
    }

    // Set landmark type
    public void SetLandmarkType(int type)
    {
        switch (type)
        {
            case 0:
                landmarkType = VirtualLandmarkType.PRIMARY;
                break;
            case 1:
                landmarkType = VirtualLandmarkType.SECONDARY;
                break;
            case 2:
                landmarkType = VirtualLandmarkType.NOT_SHOW;
                break;
            default:
                landmarkType = VirtualLandmarkType.NOT_SHOW;
                break;
        }
    }

    // Get landmark type
    public VirtualLandmarkType GetLandmarkType()
    {
        return landmarkType;
    }

    // Set connected landmark
    public void SetConnectedLandmark(int id, ref VirtualLandmark landmark)
    {
        connectedLandmarks.Add(id, landmark);
    }

    // Get connected landmark
    public VirtualLandmark GetConnectedLandmark(int id)
    {
        if(connectedLandmarks.ContainsKey(id)) {
            return connectedLandmarks[id];
        }
        return null;
    }

    // Set GameObject
    public void SetGameObject(ref GameObject gameObject)
    {
        obj = gameObject;
    }

    // Get GameObject
    public GameObject GetGameObject()
    {
        return obj;
    }

}
