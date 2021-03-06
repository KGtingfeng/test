﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{

    public static CameraMove Instance = null;

    private Vector3 dirVector3;
    private float paramater = 0.1f;
    //旋转参数
    private float xspeed = -0.05f;
    private float yspeed = 0.1f;

    private float dis;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirVector3 = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.localPosition.z <= 7.8)
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = 3;
                else dirVector3.y = 1;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(this.transform.localPosition.z>=0)
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = -3;
                else dirVector3.y = -1;
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(this.transform.localPosition.x >= 0)
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = -3;
                else dirVector3.x = -1;
            }
           
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.localPosition.x <= 8.5)
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = 3;
                else dirVector3.x = 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (this.transform.localPosition.y < 12)
                dirVector3.z = -2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (this.transform.localPosition.y > 4)
                dirVector3.z = 2;
        }

        transform.Translate(dirVector3 * paramater, Space.Self);

    }
}
