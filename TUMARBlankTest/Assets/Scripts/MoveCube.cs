using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float pos_x = 0.0f;
    public float pos_y = 0.0f;
    public float pos_z = 0.0f;

    public float rot_x = 0.0f;
    public float rot_y = 0.0f;
    public float rot_z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {

       

        Vector3 postion = new Vector3(pos_x, pos_y, pos_z);

        Quaternion rotation = Quaternion.Euler(rot_x, rot_y, rot_z);

        Pose anchorPose = new Pose(postion, rotation);

        transform.SetPositionAndRotation(anchorPose.position, anchorPose.rotation);
    }
}
