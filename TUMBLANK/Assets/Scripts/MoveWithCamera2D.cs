using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCamera2D : MonoBehaviour
{
    public Transform cameraMain;//把摄像机组件拖放到这里来
    public Transform model;// 把需要跟随摄像机视野的角色组件拖放到这里来
    public float CameraSpeed;//控制摄像机移动的速度
    public float playerSpeed;//控制跟随摄像机视野的角色的移动速度

    public Vector3 vector;//用来保存游戏一开始摄像机朝向角色的向量

    // Start is called before the first frame update
    void Start()
    {
        vector = new Vector3();
        vector += model.position - cameraMain.position;//获取朝向角色的向量
        vector.y = model.position.y;
        //Debug.Log(gameObject.name + ":" + vector.ToString());
        //Debug.Log("MoveWithCamera2D.Start()");
        //EventCenter.AddListener<Vector3>(EventType.CorrectPositionVector, CorrectPositionVector);
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

        float x = cameraMain.position.x + vector.x;
        float y = vector.y;
        float z = cameraMain.position.z + vector.z;
        Vector3 newPosition = new Vector3(x, y, z);
        model.position = newPosition;
        model.localPosition = new Vector3(model.localPosition.x, 0, model.localPosition.z);
    }

    //private void CorrectPositionVector(Vector3 correctVector)
    //{
    //    vector -= correctVector;
    //}

    //private void OnDestroy()
    //{
    //    EventCenter.RemoveListener<Vector3>(EventType.CorrectPositionVector, CorrectPositionVector);
    //}

}
