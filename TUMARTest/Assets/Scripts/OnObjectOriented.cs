using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class OnObjectOriented : MonoBehaviour
{
    string m_activatedObjType;
    string m_activatedObjName;
    bool flag_activate;
    bool flag_deactivate;
    private void Awake()
    {
        m_activatedObjType = "";
        m_activatedObjType = "";
        flag_activate = false;
        flag_deactivate = false;

        //Camera.main.transform.position= new Vector3(10, 1.5f, 10);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateActivatedObjects();
    }

    private void UpdateActivatedObjects()
    {
        

        //Vector3 startPoint = new Vector3();
        //Vector3 endPoint = new Vector3();


        // ShellHandRayPointer handPointer = PointerUtils.GetPointer<ShellHandRayPointer>(Handedness.Right);
        //if(handPointer != null && handPointer.Result != null)
        // {
        //     startPoint = handPointer.Position;
        //     endPoint = handPointer.Result.Details.Point;
        //     GameObject hitObject = handPointer.Result.Details.Object;

        //     if(hitObject)
        //     {
        //         float x = hitObject.transform.position.x;
        //         float y = hitObject.transform.position.y;
        //         float z = hitObject.transform.position.z;

        //         string s_cubeCoordinates = "";
        //         s_cubeCoordinates = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", x, y, z);
        //         EventCenter.Broadcast(EventType.ShowCubeCoordinates, s_cubeCoordinates);
        //     }

        // }


        //foreach (var source in CoreServices.InputSystem.DetectedInputSources)
        //{
        //    // Ignore anything that is not a hand because we want articulated hands
        //    if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
        //    {
        //        foreach (var p in source.Pointers)
        //        {
        //            if (p is IMixedRealityNearPointer)
        //            {
        //                // Ignore near pointers, we only want the rays
        //                continue;
        //            }
        //            if (p.Result != null)
        //            {
        //                startPoint = p.Position;
        //                endPoint = p.Result.Details.Point;
        //                //var hitObject = p.Result.Details.Object;
        //                //if (hitObject)
        //                //{
        //                //    var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //                //    sphere.transform.localScale = Vector3.one * 0.01f;
        //                //    sphere.transform.position = endPoint;
        //                //}
        //            }

        //        }
        //    }
        //}


        Vector3 Position = new Vector3();
        Vector3 Orientation = new Vector3();

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 headOrientation = Camera.main.transform.forward;

        bool flag_hit = false;
        //Store information of the hitten object
        RaycastHit hitInfo;

        Position = headPosition;
        Orientation = headOrientation;
        flag_hit = Physics.Raycast(Position, Orientation, out hitInfo);

        if (flag_hit)
        {
            string s_cubeCoordinates = "";

            //judge if new object is hitten
            if (m_activatedObjName != hitInfo.collider.gameObject.name)
            {
                //deactivate old object, if not deactivated and activated a new object.
                if (m_activatedObjName != "")
                {
                    if (m_activatedObjType == "Cube")
                    {
                        //EventCenter.Broadcast(EventType.ShowCubeDeactivated, m_activatedObjName);
                    }
                }
                //update

                //s_cubeCoordinates = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z);

                m_activatedObjName = hitInfo.collider.gameObject.name;

                //EventCenter.Broadcast(EventType.ShowCubeCoordinates, s_cubeCoordinates);

                string s_currentObjType = "Cube";

                if (s_currentObjType == "Cube")
                {
                    //EventCenter.Broadcast(EventType.ShowCubeActivated, m_activatedObjName);
                }

                m_activatedObjType = s_currentObjType;

                flag_activate = true;
                flag_deactivate = false;
            }

            float dist = hitInfo.distance;


            //hit point
            //float x = headOrientation.x * dist;
            //float y = headOrientation.y * dist;
            //float z = headOrientation.z * dist;

            //x = x + headPosition.x;
            //y = y + headPosition.y;
            //z = z + headPosition.z;

            //center
            Transform transform_hitObject = hitInfo.collider.gameObject.transform;
            float x = transform_hitObject.position.x;
            float y = transform_hitObject.position.y;
            float z = transform_hitObject.position.z;

            s_cubeCoordinates = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", x, y, z);
            EventCenter.Broadcast(EventType.ShowCubeCoordinates, s_cubeCoordinates);
        }
        else
        {
            if (!flag_deactivate)
            {
                //deactivate
                string s_cubeCoordinates = "No object detected";
                EventCenter.Broadcast(EventType.ShowCubeCoordinates, s_cubeCoordinates);
                if (m_activatedObjType == "Cube")
                {
                    //EventCenter.Broadcast(EventType.ShowCubeDeactivated, m_activatedObjName);
                }
                m_activatedObjName = "";
                flag_deactivate = true;
                flag_activate = false;
            }

        }

    }
}
