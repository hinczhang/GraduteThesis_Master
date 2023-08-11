using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDistant : MonoBehaviour
{
    Transform cameraMain;//把摄像机组件拖放到这里来
    Transform model;// 把需要跟随摄像机视野的角色组件拖放到这里来

    float hideDist = 15;
    float hdsquare;
    // Start is called before the first frame update

    void Start()
    {
        cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
        model = transform;
        hdsquare = hideDist * hideDist;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 camPos = new Vector3(cameraMain.position.x, 0, cameraMain.position.z);
        //Vector3 modPos = model.position;

        float dist = 0.0f;
        //dist = Vector3.Distance(camPos, modPos);
        float cam_pos_x = cameraMain.position.x;
        float cam_pos_y = cameraMain.position.y;
        float cam_pos_z = cameraMain.position.z;
        float mod_pos_x = model.position.x;
        float mod_pos_y = model.position.y;
        float mod_pos_z = model.position.z;

        dist = (cam_pos_x - mod_pos_x) * (cam_pos_x - mod_pos_x) + (cam_pos_z - mod_pos_z) * (cam_pos_z - mod_pos_z);

        if (dist > hdsquare)
        {
            transform.localScale = Vector3.zero;
            //gameObject.SetActive(false);
        }
        else
        {
            transform.localScale = Vector3.one;
            //gameObject.SetActive(true);
        }
     }

}
