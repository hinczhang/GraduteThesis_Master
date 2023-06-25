using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
public class SwitchMesh : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void Switch(string msg)
    {

        if (msg == "Hex")
        {
            GameObject hexObject = transform.Find("Hex").gameObject;
            bool active = hexObject.activeSelf;
            if (!active)
            {
                hexObject.SetActive(true);
            }
            else
            {
                hexObject.SetActive(false);
            }

            //GameObject hexObject = transform.Find("HexGrid").gameObject;
            //bool active = hexObject.activeSelf;
            //if (!active)
            //{
            //    hexObject.SetActive(true);
            //}
            //else
            //{
            //    hexObject.SetActive(false);
            //}
            //GameObject hexObject_Large = transform.Find("HexGrid_Large").gameObject;
            //bool active_Large = hexObject_Large.activeSelf;
            //if (!active_Large)
            //{
            //    hexObject_Large.SetActive(true);
            //}
            //else
            //{
            //    hexObject_Large.SetActive(false);
            //}
        }
        else if (msg == "Square")
        {
            GameObject squareObject = transform.Find("SquareGrid").gameObject;
            //GameObject squareObject = transform.Find("Square").gameObject;
            bool active = squareObject.activeSelf;
            if (!active)
            {
                squareObject.SetActive(true);
            }
            else
            {
                squareObject.SetActive(false);
            }

            Transform transform_Canvas = transform.parent.Find("Canvas");
            GameObject textCamTransformObject = transform_Canvas.Find("TextCamTransform").gameObject;

            bool active_text = textCamTransformObject.activeSelf;
            if (!active_text)
            {
                //textCamTransformObject.SetActive(true);

            }
            else
            {
                textCamTransformObject.SetActive(false);
            }
        }
        else if (msg == "Triangle")
        {
            GameObject triangleObject = transform.Find("TriangleGrid").gameObject;
            bool active = triangleObject.activeSelf;
            if (!active)
            {
                triangleObject.SetActive(true);
            }
            else
            {
                triangleObject.SetActive(false);
            }
        }
        else if (msg == "Circle")
        {
            GameObject triangleObject = transform.Find("CircleGrid").gameObject;
            bool active = triangleObject.activeSelf;
            if (!active)
            {
                triangleObject.SetActive(true);
            }
            else
            {
                triangleObject.SetActive(false);
            }
        }
        else if (msg == "Cube")
        {
            GameObject cubeObject = transform.Find("Cubes").Find("CubeObjects").gameObject;
            bool active = cubeObject.activeSelf;
            if (!active)
            {
                cubeObject.SetActive(true);
            }
            else
            {
                cubeObject.SetActive(false);
            }
            //GameObject anchorCubeObject = transform.Find("AnchorCubes").gameObject;
            //bool active_Anchor = anchorCubeObject.activeSelf;
            //if (!active_Anchor)
            //{
            //    anchorCubeObject.SetActive(true);
            //}
            //else
            //{
            //    anchorCubeObject.SetActive(false);
            //}

        }
        else if (msg == "Slider")
        {
            Transform transform_UI = transform.parent.Find("UI");
            GameObject sliderObjectUI = transform_UI.Find("SliderUI").gameObject;
            bool active = sliderObjectUI.activeSelf;
            if (!active)
            {
                sliderObjectUI.SetActive(true);
            }
            else
            {
                sliderObjectUI.SetActive(false);
            }
            //Transform transform_Canvas = transform.parent.Find("Canvas");
            //GameObject textCubeInfoObject = transform_Canvas.Find("TextCubeInfo").gameObject;
            //bool activeTextCubeInfo = textCubeInfoObject.activeSelf;
            //if (!activeTextCubeInfo)
            //{
            //    textCubeInfoObject.SetActive(true);
            //}
            //else
            //{
            //    textCubeInfoObject.SetActive(false);
            //}
        }
        else if (msg == "Occlusion")
        {
            GameObject occlusionObject = transform.Find("OcclusionPlanes").gameObject;
            bool active = occlusionObject.activeSelf;
            if (!active)
            {
                occlusionObject.SetActive(true);
            }
            else
            {
                occlusionObject.SetActive(false);
            }
        }
        else if (msg == "Text")
        {
            Transform transform_Canvas = transform.parent.Find("Canvas");
            GameObject textCamTransformObject = transform_Canvas.Find("TextCamTransform").gameObject;

            
            bool active = textCamTransformObject.activeSelf;
            if (!active)
            {
                textCamTransformObject.SetActive(true);
                
            }
            else
            {
                textCamTransformObject.SetActive(false);
            }
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

    //        if (Input.GetKeyDown(KeyCode.Keypad1))
    //        {
    //            GameObject hexObject = transform.Find("HexGrid").gameObject;
    //            bool active = hexObject.activeSelf;
    //            if (!active)
    //            {
    //                hexObject.SetActive(true);
    //            }
    //            else
    //            {
    //                hexObject.SetActive(false);
    //            }
    //            GameObject hexObject_Large = transform.Find("HexGrid_Large").gameObject;
    //            bool active_Large = hexObject_Large.activeSelf;
    //            if (!active_Large)
    //            {
    //                hexObject_Large.SetActive(true);
    //            }
    //            else
    //            {
    //                hexObject_Large.SetActive(false);
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad2))
    //        {
    //            GameObject squareObject = transform.Find("SquareGrid").gameObject;
    //            bool active = squareObject.activeSelf;
    //            if (!active)
    //            {
    //                squareObject.SetActive(true);
    //            }
    //            else
    //            {
    //                squareObject.SetActive(false);
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad3))
    //        {
    //            //GameObject triangleObject = transform.Find("TriangleGrid").gameObject;
    //            //bool active = triangleObject.activeSelf;
    //            //if (!active)
    //            //{
    //            //    triangleObject.SetActive(true);
    //            //}
    //            //else
    //            //{
    //            //    triangleObject.SetActive(false);
    //            //}
    //            GameObject circleObject = transform.Find("CircleGrid").gameObject;
    //            bool active = circleObject.activeSelf;
    //            if (!active)
    //            {
    //                circleObject.SetActive(true);
    //            }
    //            else
    //            {
    //                circleObject.SetActive(false);
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad4))
    //        {
    //            GameObject cubeObject = transform.Find("Cubes").gameObject;
    //            bool active = cubeObject.activeSelf;
    //            if (!active)
    //            {
    //                cubeObject.SetActive(true);
    //            }
    //            else
    //            {
    //                cubeObject.SetActive(false);
    //            }
    //            GameObject anchorCubeObject = transform.Find("AnchorCubes").gameObject;
    //            bool active_Anchor = anchorCubeObject.activeSelf;
    //            if (!active_Anchor)
    //            {
    //                anchorCubeObject.SetActive(true);
    //            }
    //            else
    //            {
    //                anchorCubeObject.SetActive(false);
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad5))
    //        {
    //            GameObject occlusionObject = transform.Find("OcclusionPlanes").gameObject;
    //            bool active = occlusionObject.activeSelf;
    //            if (!active)
    //            {
    //                occlusionObject.SetActive(true);
    //            }
    //            else
    //            {
    //                occlusionObject.SetActive(false);
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad6))
    //        {
    //            Transform transform_UI = transform.parent.Find("UI");
    //            GameObject sliderObjectUI = transform_UI.Find("SliderUI").gameObject;             
    //            bool active = sliderObjectUI.activeSelf;
    //            if (!active)
    //            {
    //                sliderObjectUI.SetActive(true);
    //            }
    //            else
    //            {
    //                sliderObjectUI.SetActive(false);
    //            }

    //        }
    //        else if (Input.GetKeyDown(KeyCode.Keypad7))
    //        {
    //            Transform transform_Canvas = transform.parent.Find("Canvas");
    //            GameObject textCamTransformObject = transform_Canvas.Find("SliderUI").gameObject;
    //            bool active = textCamTransformObject.activeSelf;
    //            if (!active)
    //            {
    //                textCamTransformObject.SetActive(true);
    //            }
    //            else
    //            {
    //                textCamTransformObject.SetActive(false);
    //            }
    //        }
    //        break;
    //    }
    //}

}



