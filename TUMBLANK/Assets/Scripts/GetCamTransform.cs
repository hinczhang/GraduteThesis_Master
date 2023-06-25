using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class GetCamTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetTransform(string msg)
    {

        if (msg == "Position")
        {
            float pos_x = transform.position.x;
            float pos_y = transform.position.y;
            float pos_z = transform.position.z;

            //string s_Transform = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", pos_x, pos_y, pos_z);
            string s_Transform = string.Format("x={0:f2}\ny={1:f2}", pos_x, pos_z);
            EventCenter.Broadcast(EventType.ShowCamTransform, s_Transform);
        }
        else if (msg == "Orientation")
        {
            float for_x = transform.forward.x;
            float for_y = transform.forward.y;
            float for_z = transform.forward.z;

            //string s_Transform = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", for_x, for_y, for_z);
            string s_Transform = string.Format("x={0:f2}\ny={1:f2}", for_x, for_z);
            EventCenter.Broadcast(EventType.ShowCamTransform, s_Transform);
        }
    }

    //void OnGUI()
    //{
    //    while (true)
    //    {
    //        if (!Input.anyKeyDown)
    //        {
    //            break;
    //        }
    //        Event evt = Event.current;

    //        if (!evt.isKey)
    //        {
    //            break;
    //        }
    //        KeyCode currentKey = evt.keyCode;
    //        if (currentKey.ToString() == "None")
    //        {
    //            break;
    //        }

    //        if (Input.GetKeyDown(KeyCode.Keypad8))
    //        {
    //            float pos_x = transform.position.x;
    //            float pos_y = transform.position.y;
    //            float pos_z = transform.position.z;

    //            string s_Transform = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", pos_x, pos_y, pos_z);
    //            EventCenter.Broadcast(EventType.ShowCamTransform, s_Transform);
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad9))
    //        {
    //            float for_x = transform.forward.x;
    //            float for_y = transform.forward.y;
    //            float for_z = transform.forward.z;

    //            string s_Transform = string.Format("x={0:f2}, y={1:f2}, z={2:f2}", for_x, for_y, for_z);
    //            EventCenter.Broadcast(EventType.ShowCamTransform, s_Transform);
    //        }
    //        break;

    //    }
    //}

}
