using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Objects
    public Transform Camera;    //access transfrm info for Camera
    CharacterController mover;  // allows us to access the character controller through the variable mover


    //Input
    Vector2 input; //establish vector2 tocontrol the X and Z values

    //Camera
    Vector3 camF; //establish camera forward so all functions may access it
    Vector3 camR; //establish camera Right so all functions may access it


    //Physics
    Vector3 intent; // establishes variable intent so that other functions may refer to it
    Vector3 velocity; // change in position over time
    Vector3 velocityXZ; // change in position over time
    float accel = 15.0f; // used in velocity later on to determine the speed at which we reach the intended velocity
    float speed = 15.0f; // movement speed
    float turnSpeed = 5.0f;
    float turnSpeedMin = 5.0f; //min speed at which we turn to our intended directions
    float turnSpeedMax = 20.0f; //max speed at which we turn to our intended directions

    //Gravity
    float grav = 50;
    bool onFloor;
    float jumpHeight = 22;

    void Start()
    {
        mover = GetComponent<CharacterController>(); // allows us to access the character controller through the variable mover
    }

    void Update()
    {
        PlayerInput();
        CalcCam();
        CalcGround();
        MovePlayer();
        Gravity();
        Jump();
       

        mover.Move(velocity * Time.deltaTime); //move by velocity
    }

    void PlayerInput()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // establishes input as two possible options, X and Z axis(due to the nature of vector2, Z axis is actually represented as the y axis)
        input = Vector2.ClampMagnitude(input, 1); //magnitude can not exceed 1, so diagonals are cool
    }

    void CalcCam()
    {
        camF = Camera.forward; //camF is the forward direction of the camera or the relative Z axis 

        camR = Camera.right; //camR is the relative X axis

        camF.y = 0; //negates the Y-axis rotation
        camR.y = 0;
        camF = camF.normalized; //normalizes variables making it a magnitude of 1, while preserving direction
        camR = camR.normalized;
    }

    void CalcGround()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.2f))
        {

            onFloor = true;

        }
        else
        {

            onFloor = false;

        }

    }

    void MovePlayer()
    {
        // transform.position += new Vector3(input.x,0, input.y)*Time.deltaTime*speed; //its listedas input.y for the Z value only because its coming from a Vector2

        // transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * speed;

        intent = camF * input.y + camR * input.x; //intent is the direction of movement reliant on the camera position

        float ts = velocityXZ.magnitude / speed;
        turnSpeed = Mathf.Lerp(turnSpeedMax, turnSpeedMin, ts);


        if (input.magnitude > 0) //if moving forward or backward (input.magnitude will always be 1 or 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent); // rotation 'rot' is the direction of intent
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime); //rotate to the intended position at a rate of variable turnspeed
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * speed, accel); // always move forward (magnitude is positive).  move towards the final velocity at a rate of acceleration
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);




    }

    void Gravity()
    {
        if (onFloor == true)
        {
            velocity.y = -0.9f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -20f, 20); //max velocity.y )
        }
    }

    void Jump()
    {
        
        if (mover.isGrounded)
        {

            if (Input.GetButtonDown("Jump"))
            {
               
                velocity.y = jumpHeight;

            }

        }

    }
}
