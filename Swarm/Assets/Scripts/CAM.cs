using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour {

    public Transform player;
    float heading = 0; // Y-rotation of our camera
    float rotSpeed = 360.0f; // mouse sensitivity
    float tilt = 30f;
    float camDist = 25f;
    float playerHeight = 1;

    void LateUpdate ()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed; //the value of the heading is changed by the input of mouse X, multiplied by the sensativity and deltatime
        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        tilt -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        tilt = Mathf.Clamp(tilt, 0, 89);

        transform.position = player.position - transform.forward * camDist + Vector3.up * playerHeight;
    }
}
