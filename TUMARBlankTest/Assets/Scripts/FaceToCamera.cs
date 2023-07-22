using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    Transform cameraMain;//把摄像机组件拖放到这里来
    Transform model;// 把需要跟随摄像机视野的角色组件拖放到这里来
    Vector3 vector;
    Vector3 forwardMod;
    Vector3 forwardCam;
    float org_y;

    float totalTime;
    int time = 120;
    int minute = -1;
    int sec = -1;
    public float repeatTime = 1.0f;

    public float hideDist = -1.0f;
    float hdsquare;
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
        hdsquare = hideDist * hideDist;

        InvokeRepeating("Timer", 1, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Timer()
    {
        ////Countdown
        //if(time > 0)
        //{
        //    time--;
        //    minute = time / 60;
        //    sec = time % 60;
        //}
        //else
        //{
        //    ChangeModelForward();
        //}

        //if (minute==0 && sec == 0)
        //{
        //    ChangeModelForward();
        //}

        ChangeModelForward();
    }

    void ChangeModelForward()
    {
        float cam_pos_x = cameraMain.position.x;
        float cam_pos_y = cameraMain.position.y;
        float cam_pos_z = cameraMain.position.z;
        float mod_pos_x = model.position.x;
        float mod_pos_y = model.position.y;
        float mod_pos_z = model.position.z;

        float dist = 0.0f;
        dist = (cam_pos_x - mod_pos_x) * (cam_pos_x - mod_pos_x) + (cam_pos_z - mod_pos_z) * (cam_pos_z - mod_pos_z);

        if (hideDist > 0)
        {
            if (dist > hideDist * hideDist)
            {
                return;
            }
        }

        float new_for_x = mod_pos_x - cam_pos_x;
        //float new_for_y = model.forward.y;
        float new_for_y = mod_pos_y - cam_pos_y;
        float new_for_z = mod_pos_z - cam_pos_z;
        model.forward = new Vector3(new_for_x, new_for_y, new_for_z);
    }
}

