using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    public Transform cameraMain;//把摄像机组件拖放到这里来
    public Transform model;// 把需要跟随摄像机视野的角色组件拖放到这里来
    Vector3 vector;
    Vector3 forwardMod;
    Vector3 forwardCam;
    float org_y;
    // Start is called before the first frame update

    //RotateWithCamera(Transform cameraMain,Transform model)
    //{
    //    this.cameraMain = cameraMain;
    //    this.model = model;
    //}

    void Start()
    {
        cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
        model = transform;
        vector = model.position - cameraMain.position;//获取朝向角色的向量
        forwardMod = model.forward;
        forwardCam = cameraMain.forward;
        org_y = Mathf.Atan2(forwardCam.z, forwardCam.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curForwardCam = cameraMain.forward;
        float cur_y = Mathf.Atan2(curForwardCam.z, curForwardCam.x);
        float diff_y = cur_y - org_y;

        if (diff_y != 0)
        {
            float cam_pos_x = cameraMain.position.x;
            float cam_pos_y = cameraMain.position.y;
            float cam_pos_z = cameraMain.position.z;
            float new_pos_x = Mathf.Cos(diff_y) * vector.x - Mathf.Sin(diff_y) * vector.z + cam_pos_x;
            float new_pos_y = cam_pos_y + vector.y;
            float new_pos_z = Mathf.Sin(diff_y) * vector.x + Mathf.Cos(diff_y) * vector.z + cam_pos_z;
            model.position = new Vector3(new_pos_x, new_pos_y, new_pos_z);

           
            float new_for_x = Mathf.Cos(diff_y) * forwardMod.x - Mathf.Sin(diff_y) * forwardMod.z;
            float new_for_y = model.forward.y;
            float new_for_z = Mathf.Sin(diff_y) * forwardMod.x + Mathf.Cos(diff_y) * forwardMod.z;
            model.forward = new Vector3(new_for_x, new_for_y, new_for_z);

        }

    }
}
