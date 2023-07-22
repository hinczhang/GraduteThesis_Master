using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCamera : MonoBehaviour
{
    public Transform cameraMain;//把摄像机组件拖放到这里来
    public Transform model;// 把需要跟随摄像机视野的角色组件拖放到这里来
    public float CameraSpeed;//控制摄像机移动的速度
    public float playerSpeed;//控制跟随摄像机视野的角色的移动速度

    Vector3 vector;//用来保存游戏一开始摄像机朝向角色的向量

    // Start is called before the first frame update
    void Start()
    {
        vector = model.position - cameraMain.position;//获取朝向角色的向量
        //Debug.Log(gameObject.name + ":" + vector.ToString());
        //Debug.Log("MoveWithCamera.Start()");
    }

    // Update is called once per frame
    void Update()
    {

        //float posX = Input.GetAxis("Horizontal");
        //float posY = Input.GetAxis("Vertical");

        //vector = model.position - cameraMain.position;//获取朝向角色的向量

        //cameraMain.position += new Vector3(posX, 0, posY) * Time.deltaTime * CameraSpeed;
        //Vector3 playerPos = Vector3.Lerp(model.position, cameraMain.position + vector, Time.deltaTime);
        //Vector3 playerPos = model.position + vector;
        model.position = cameraMain.position + vector;
    }
}
