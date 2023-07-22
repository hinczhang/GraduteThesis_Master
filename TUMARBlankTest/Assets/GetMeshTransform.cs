using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeshTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start is called before the first frame update

    public void GetTransform(string msg)
    {

        if (msg == "Mesh")
        {
            float pos_x = transform.position.x;
            float pos_y = transform.position.y;
            float pos_z = transform.position.z; 

            float rot_x = transform.rotation.eulerAngles.x;
            float rot_y = transform.rotation.eulerAngles.y;
            float rot_z = transform.rotation.eulerAngles.z;
            //string s_Transform = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", pos_x, pos_y, pos_z);
            string s_Transform = string.Format("x = {0:f2}, y = {1:f2}, z = {2:f2}\nrx = {3:f2}, ry = {4:f2}, rz = {5:f2}", pos_x, pos_y, pos_z, rot_x, rot_y, rot_z);
            EventCenter.Broadcast(EventType.ShowCamTransform, s_Transform);

        }
    }
}
