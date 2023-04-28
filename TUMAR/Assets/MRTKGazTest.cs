using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class MRTKGazTest : MonoBehaviour, IMixedRealityFocusHandler
{
    private MeshRenderer cubeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.TryGetComponent<MeshRenderer>(out cubeRenderer)) {
            cubeRenderer = gameObject.GetComponent<MeshRenderer>();
        } else {
            cubeRenderer = gameObject.AddComponent<MeshRenderer>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        cubeRenderer.material.color = Color.red;
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        cubeRenderer.material.color = Color.green;
    }
}
