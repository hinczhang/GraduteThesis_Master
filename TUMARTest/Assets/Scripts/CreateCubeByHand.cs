using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class CreateCubeByHand : MonoBehaviour
{
    private int newCubeNum;
    private GameObject OrgCube;
    private List<GameObject> cubeList;
    // Start is called before the first frame update
    void Start()
    {
        newCubeNum = 0;
        OrgCube = (GameObject)Resources.Load("Prefabs/OrgCube");
        cubeList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Cube(string msg)
    {
        if (msg == "Create")
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
                //GameObject OrgCube = (GameObject)Resources.Load("Prefabs/OrgCube");
                GameObject OrgCube = (GameObject)Resources.Load("Prefabs/OrgCube");
                //newCube.transform.localPosition = new Vector3(x, y, z);
                Vector3 posVec = new Vector3(x, y, z);
                Quaternion rotQtn = new Quaternion(0, 0, 0, 0);
                GameObject parentObject = GameObject.Find("ObjectCollection");
                //GameObject parentObject = GameObject.Find("Cubes");
                GameObject newCube = (GameObject)Instantiate(OrgCube, posVec, rotQtn);
                //newCube.transform.localScale = scale;
                newCube.transform.parent = parentObject.transform;
                newCube.name = "RecalledObject" + newCubeNum;
                newCubeNum++;
                cubeList.Add(newCube);
                string s_log = string.Format("Object {0}: x={1}, y={2}, z={3}", newCubeNum, posVec.x, posVec.y, posVec.z);
                EventCenter.Broadcast(EventType.ShowCamTransform, s_log);
                //Debug.Log(s_log);
            }
        }
        if (msg == "Delete")
        {
            if (cubeList.Count > 0)
            {
                Destroy(cubeList[cubeList.Count - 1]);
                cubeList.RemoveAt(cubeList.Count - 1);
                newCubeNum--;
            }
        }

    }
}
