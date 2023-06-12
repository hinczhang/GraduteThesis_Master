using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    private int newCubeNum;
    private GameObject OrgCube;
    private void Awake()
    {
        newCubeNum = 3;
        OrgCube = (GameObject)Resources.Load("Prefabs/OrgCube");
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            //Debug.Log("Hotkey Activated");

            //Head position
            Vector3 headPosition = Camera.main.transform.position;

            //Head orientation
            Vector3 headOrientation = Camera.main.transform.forward;

            //Store information of the hitten object
            RaycastHit hitInfo;
            bool flag_hit = Physics.Raycast(headPosition, headOrientation, out hitInfo);

            float dist = 5;
            if (flag_hit && hitInfo.distance < dist)
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

            if (y < 0)
            {
                y = 0;
            }

            //Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

            //Loading prefab resources
            GameObject OrgCube = (GameObject)Resources.Load("Prefabs/OrgCube");
            //newCube.transform.localPosition = new Vector3(x, y, z);
            Vector3 posVec = new Vector3(x, y, z);
            Quaternion rotQtn = new Quaternion(0, 0, 0, 0);
            //GameObject parentObject = GameObject.Find("CubeCollection");
            GameObject parentObject = GameObject.Find("Cubes");

            GameObject newCube = (GameObject)Instantiate(OrgCube, posVec, rotQtn);
            //newCube.transform.localScale = scale;
            newCube.transform.parent = parentObject.transform;
            newCube.name = "Cube" + newCubeNum;
            newCubeNum++;

            string s_log = string.Format("{0}: x={1}, y={2}, z={3}", newCube.name, posVec.x, posVec.y, posVec.z);
            //Debug.Log(s_log);

            //cubeObject.transform.parent = ;

            //Destroy(cubeObject.gameObject, 1f);
        }
    }
}