using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerbob : MonoBehaviour
{

    float bobDegree;

    bool goingUP = true;

    Transform upBob;

    Transform downBob;

    float bobspeed = 5f;

    public CharacterController mover;

    // Start is called before the first frame update
    void Start()
    {
        upBob = GameObject.Find("upBob").GetComponent<Transform>();
        downBob = GameObject.Find("downBob").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bobDegree = 1;


        if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && mover.isGrounded)
        {
            Bob();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, downBob.position, Time.deltaTime * 10);
        }
    }

    

    void Bob()
    {

        if (transform.position.y >= upBob.position.y)
        {
            goingUP = false;
        }
        if (transform.position.y <= downBob.position.y)
        {
            goingUP = true;
        }

        if (goingUP)
        {
            transform.position += new Vector3(0, bobDegree, 0) * Time.deltaTime * bobspeed;
        }
        else
        {
            transform.position -= new Vector3(0, bobDegree, 0) * Time.deltaTime * bobspeed;
        }
        
    }
}
