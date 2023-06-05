using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SetOriginTransform : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 originPos;

    void Start()
    {
        //originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRotation(SliderEventData eventData)
    {
        float degree = 0.0f;
        degree = (eventData.NewValue - 0.5f) * 2 * 30;
        Quaternion target = Quaternion.Euler(0, degree, 0);
        transform.rotation = target;
    }

    public void SetPositionX(SliderEventData eventData)
    {
        float dx = 0.0f;
        dx = (eventData.NewValue - 0.5f) * 2 * 5;
        Vector3 target = new Vector3(dx, 0, transform.position.z);
        transform.position = originPos + target;
    }
    public void SetPositionZ(SliderEventData eventData)
    {
        float dz = 0.0f;
        dz = (eventData.NewValue - 0.5f) * 2 * 5;
        Vector3 target = new Vector3(transform.position.x, 0, dz);
        transform.position = originPos + target;
    }
}
