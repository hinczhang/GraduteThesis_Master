using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class OnLandmarkOriented : MonoBehaviour
{
    string m_activatedObjName;
    string m_activatedObjType;
    bool flag_activate;
    bool flag_deactivate;
    float refresh_timeLeft;

    private void Awake()
    {
        m_activatedObjName = "";
        m_activatedObjType = "";
        flag_activate = false;
        flag_deactivate = false;
        refresh_timeLeft = 0.2f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        refresh_timeLeft -= Time.deltaTime;
        if (refresh_timeLeft < 0)
        {
            //UpdateActivatedObjects();
            CheckOrientedLandmark();
            refresh_timeLeft = 0.2f;
        }
    }


    private void
        CheckOrientedLandmark()
    {
        float desiredAngle = 30;
        float desiredRadian = desiredAngle / 180 * Mathf.PI;
        float cos_desiredRadian = Mathf.Cos(desiredRadian);
        List<GameObject> InAngle = new List<GameObject>();
        GameObject[] landmarkArray = GameObject.FindGameObjectsWithTag("Tag_Hit");
        for (int i = 0; i < landmarkArray.Length; i++)
        {
            GameObject tested = landmarkArray[i];

            Vector3 dir = tested.transform.position - transform.position;
            //float cos_dir = Mathf.Abs(Vector3.Dot(dir, transform.forward)) / dir.magnitude / transform.forward.magnitude;

            //if (cos_dir >= cos_desiredRadian)
            //{
            //    InAngle.Add(tested);
            //}

            float angle = Vector3.Angle(dir, transform.forward);
            //Debug.Log("Angle = " + angle);
            if (angle <= desiredAngle)
            {
                InAngle.Add(tested);
            }
        }

        if (InAngle.Count < 1)
        {
            //Debug.Log("InAngle.Count < 1");
            if (flag_activate)
            {
                //deactivate
                DeactivateLandmark(m_activatedObjName);
                m_activatedObjName = "";
                flag_activate = false;
            }
            return;
        }
        GameObject closest = InAngle[0];
        //Debug.Log("InAngle.Count >= 1");
        for (int j = 1; j < InAngle.Count; j++)
        {
            GameObject tested = InAngle[j];

            Vector3 dir_tested = tested.transform.position - transform.position;
            Vector3 dir_closest = closest.transform.position - transform.position;
            float cos_dir_tested = Vector3.Dot(dir_tested, transform.forward) / dir_tested.magnitude / transform.forward.magnitude;
            float cos_dir_closest = Vector3.Dot(dir_closest, transform.forward) / dir_closest.magnitude / transform.forward.magnitude;
            if (cos_dir_closest < cos_dir_tested)
            {
                closest = tested;
            }

        }
        string name_landmark = closest.transform.parent.name;
        //judge if new object is hitten
        if (m_activatedObjName != name_landmark)
        {
            if (flag_activate)
            {
                //deactivate
                DeactivateLandmark(m_activatedObjName);
            }
            //update and activate
            m_activatedObjName = name_landmark;
            ActivateLandmark(m_activatedObjName);
            flag_activate = true;
        }
    }

    private void ActivateLandmark(string str)
    {
        Transform transform_Canvas = GameObject.Find("Canvas").transform;
        GameObject textCamTransformObject = transform_Canvas.Find("TextCamTransform").gameObject;
        textCamTransformObject.SetActive(true);
        EventCenter.Broadcast(EventType.ShowCamTransform, str);


        GameObject obj_landmark = GameObject.Find(str);

        ActivateLandmarkBorder(obj_landmark);
        ActivateLandmarkText(obj_landmark);

        //Debug.Log("ActivateLandmark");
    }


    private void DeactivateLandmark(string str)
    {
        Transform transform_Canvas = GameObject.Find("Canvas").transform;
        GameObject textCamTransformObject = transform_Canvas.Find("TextCamTransform").gameObject;
        textCamTransformObject.SetActive(false);
        //EventCenter.Broadcast(EventType.ShowCamTransform, "Not hitten");
                

        GameObject obj_landmark = GameObject.Find(str);

        DeactivateLandmarkBorder(obj_landmark);
        DeactivateLandmarkText(obj_landmark);

        

        //Debug.Log("DeactivateLandmark");
    }


    private void ActivateLandmarkBorder(GameObject obj)
    {
        string name = "";
        bool flag_hasBorder = false;
        foreach (Transform child in obj.transform)
        {
            name = child.name;
            flag_hasBorder = name.Contains("Border");
            if (flag_hasBorder)
            {
                child.gameObject.SetActive(true);
                break;
            }
        }
    }


    private void DeactivateLandmarkBorder(GameObject obj)
    {
        string name = "";
        bool flag_hasBorder = false;
        foreach (Transform child in obj.transform)
        {
            name = child.name;
            flag_hasBorder = name.Contains("Border");
            if (flag_hasBorder)
            {
                child.gameObject.SetActive(false);
                break;
            }
        }
    }


    private void ActivateLandmarkText(GameObject obj)
    {
        TextMeshPro t_text = new TextMeshPro();
        bool flag_hasTMP = false;
        foreach (Transform child in obj.transform)
        {
             flag_hasTMP = child.TryGetComponent<TextMeshPro>(out t_text);
            if (flag_hasTMP)
            {
                t_text.color = Color.red;
                break;
            }
        }
    }


    private void DeactivateLandmarkText(GameObject obj)
    {
        TextMeshPro t_text = new TextMeshPro();
        bool flag_hasTMP = false;
        foreach (Transform child in obj.transform)
        {
            flag_hasTMP = child.TryGetComponent<TextMeshPro>(out t_text);
            if (flag_hasTMP)
            {
                t_text.color = Color.white;
                break;
            }
        }
    }


    private void UpdateActivatedObjects()
    {

        /*
        vector3 startpoint = new vector3();
        vector3 endpoint = new vector3();


        shellhandraypointer handpointer = pointerutils.getpointer<shellhandraypointer>(handedness.right);
        if (handpointer != null && handpointer.result != null)
        {
            startpoint = handpointer.position;
            endpoint = handpointer.result.details.point;
            gameobject hitobject = handpointer.result.details.object;

            if (hitobject)
            {
                float x = hitobject.transform.position.x;
                float y = hitobject.transform.position.y;
                float z = hitobject.transform.position.z;

                string s_cubecoordinates = "";
                s_cubecoordinates = string.format("x={0:f2}, y={1:f2}, z={2:f2}", x, y, z);
                eventcenter.broadcast(eventtype.showcubecoordinates, s_cubecoordinates);
            }

        }

        foreach (var source in coreservices.inputsystem.detectedinputsources)
        {
            // ignore anything that is not a hand because we want articulated hands
            if (source.sourcetype == microsoft.mixedreality.toolkit.input.inputsourcetype.hand)
            {
                foreach (var p in source.pointers)
                {
                    if (p is imixedrealitynearpointer)
                    {
                        // ignore near pointers, we only want the rays
                        continue;
                    }
                    if (p.result != null)
                    {
                        startpoint = p.position;
                        endpoint = p.result.details.point;
                        //var hitobject = p.result.details.object;
                        //if (hitobject)
                        //{
                        //    var sphere = gameobject.createprimitive(primitivetype.sphere);
                        //    sphere.transform.localscale = vector3.one * 0.01f;
                        //    sphere.transform.position = endpoint;
                        //}
                    }

                }
            }
        }
        */

        Vector3 Position = new Vector3();
        Vector3 Orientation = new Vector3();

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 headOrientation = Camera.main.transform.forward;

        bool flag_hit = false;
        //Store information of the hitten object
        RaycastHit hitInfo;
        int layer_mask = LayerMask.GetMask("PointerHit");

        Position = headPosition;
        Orientation = headOrientation;

        flag_hit = Physics.Raycast(Position, Orientation, out hitInfo, Mathf.Infinity, layer_mask);

        if (flag_hit)
        {
            string tag_obj = hitInfo.collider.gameObject.tag;
            string name_obj = hitInfo.collider.gameObject.name;
            Debug.Log(name_obj);
            if (tag_obj == "Tag_Hit")
            {
                string name_landmark = hitInfo.collider.gameObject.transform.parent.name;
                //judge if new object is hitten
                if (m_activatedObjName != name_landmark)
                {
                    //update and activate
                    m_activatedObjName = name_landmark;
                    ActivateLandmark(m_activatedObjName);
                    flag_activate = true;
                }
            }
            else
            {
                if (flag_activate)
                {
                    //deactivate
                    DeactivateLandmark(m_activatedObjName);
                    m_activatedObjName = "";
                    flag_activate = false;
                }

            }
        }
        else
        {
            if (flag_activate)
            {
                //deactivate
                DeactivateLandmark(m_activatedObjName);
                m_activatedObjName = "";
                flag_activate = false;
            }

        }

    }
}
