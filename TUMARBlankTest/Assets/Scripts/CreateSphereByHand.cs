using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class CreateSphereByHand : MonoBehaviour, IMixedRealitySpeechHandler
{
    private int newSphereNum;
    private GameObject OrgSphere;

    // Start is called before the first frame update
    void Start()
    {
        newSphereNum = 3;
        OrgSphere = (GameObject)Resources.Load("Prefabs/OrgSphere");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateSphere(string msg)
    {
        if (msg == "Create Marker")
        {

            bool flag_hit = false;

            Vector3 endPoint = new Vector3();
            flag_hit = PointerUtils.TryGetPointerEndpoint<ShellHandRayPointer>(Handedness.Right, out endPoint);
                        
            float dist = 5;
            if (flag_hit)
            {


                float x = 0f;
                float y = 0f;
                float z = 0f;

                x = endPoint.x;
                y = endPoint.y;
                z = endPoint.z;

                //Loading prefab resources
                //GameObject OrgSphere = (GameObject)Resources.Load("Prefabs/OrgSphere");
                GameObject OrgSphere = (GameObject)Resources.Load("Prefabs/OrgCube");
                //newSphere.transform.localPosition = new Vector3(x, y, z);
                Vector3 posVec = new Vector3(x, y, z);
                Quaternion rotQtn = new Quaternion(0, 0, 0, 0);
                //GameObject parentObject = GameObject.Find("SphereCollection");
                //GameObject parentObject = GameObject.Find("CUbeCollection");
                GameObject parentObject = GameObject.Find("Cubes");
                GameObject newSphere = (GameObject)Instantiate(OrgSphere, posVec, rotQtn);
                //newSphere.transform.localScale = scale;
                newSphere.transform.parent = parentObject.transform;
                newSphere.name = "AnchorCube" + newSphereNum;
                newSphereNum++;

                string s_log = string.Format("{0}: x={1}, y={2}, z={3}", newSphere.name, posVec.x, posVec.y, posVec.z);
                //Debug.Log(s_log);
            }
        }
     
    }
    void IMixedRealitySpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        Debug.Log("SpeechKeywordRecognized");

        //if (Input.GetKeyDown(KeyCode.M))
        if (eventData.Command.Keyword == "Create Marker")
        {
            Debug.Log("SpeechKeyword Activated");
            //Head position
            Vector3 headPosition = Camera.main.transform.position;

            //Head orientation
            Vector3 headOrientation = Camera.main.transform.forward;

            //Store information of the hitten object
            RaycastHit hitInfo;
            bool flag_hit = Physics.Raycast(headPosition, headOrientation, out hitInfo);

            float dist = 5;
            if (flag_hit)
            {
                if (hitInfo.distance < dist)
                {
                    dist = hitInfo.distance;
                    //Debug.Log(dist);
                }

                float x = headOrientation.x * dist;
                float y = headOrientation.y * dist;
                float z = headOrientation.z * dist;

                x = x + headPosition.x;
                y = y + headPosition.y;
                z = z + headPosition.z;



                //Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

                //Loading prefab resources
                GameObject OrgSphere = (GameObject)Resources.Load("Prefabs/OrgSphere");
                //newSphere.transform.localPosition = new Vector3(x, y, z);
                Vector3 posVec = new Vector3(x, y, z);
                Quaternion rotQtn = new Quaternion(0, 0, 0, 0);
                GameObject parentObject = GameObject.Find("SphereCollection");

                GameObject newSphere = (GameObject)Instantiate(OrgSphere, posVec, rotQtn);
                //newSphere.transform.localScale = scale;
                newSphere.transform.parent = parentObject.transform;
                newSphere.name = "Sphere" + newSphereNum;
                newSphereNum++;

                string s_log = string.Format("{0}: x={1}, y={2}, z={3}", newSphere.name, posVec.x, posVec.y, posVec.z);
                //Debug.Log(s_log);

                //SphereObject.transform.parent = ;

                //Destroy(SphereObject.gameObject, 1f);
            }
        }
    }
}