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
    public List<int> connectedIDs;
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
    private List<VirtualLandmark> connectedLandmarks = new List<VirtualLandmark>();
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
    public void AddConnectedLandmark(VirtualLandmark landmark)
    {
        connectedLandmarks.Add(landmark);
    }

    // Get connected landmarks
    public List<VirtualLandmark> GetConnectedLandmarks()
    {
        return connectedLandmarks;
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
