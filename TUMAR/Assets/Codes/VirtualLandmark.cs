using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VirtualLandmarkType
{
    PRIMARY,
    SECONDARY
};


public class VirtualLandmark : MonoBehaviour
{
    private string landmarkName;
    private string landmarkDescription;
    private VirtualLandmarkType landmarkType;
    private List<VirtualLandmark> connectedLandmarks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set landmark name
    public void SetLandmarkName(string name)
    {
        landmarkName = name;
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

    // Set landmark type
    public void SetLandmarkType(VirtualLandmarkType type)
    {
        landmarkType = type;
    }

    // Get landmark type
    public VirtualLandmarkType GetLandmarkType()
    {
        return landmarkType;
    }

    // Set connected landmarks
    public void AddConnectedLandmarks(ref VirtualLandmark landmark)
    {
        if (connectedLandmarks == null)
        {
            connectedLandmarks = new List<VirtualLandmark>();
        }
        connectedLandmarks.Add(landmark);
    }

    // Get connected landmark by index
    public VirtualLandmark GetConnectedLandmark(int index)
    {
        if (index >= connectedLandmarks.Count)
        {
            return null;
        }
        return connectedLandmarks[index];
    }

    // Get connected landmark by name
    public VirtualLandmark GetConnectedLandmark(string name)
    {
        foreach (VirtualLandmark landmark in connectedLandmarks)
        {
            if (landmark.GetLandmarkName() == name)
            {
                return landmark;
            }
        }
        return null;
    }
}
