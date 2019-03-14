using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyFollow : MonoBehaviour
{

    CharacterController mover;
    float speed = 10f;

    bool closeEnough = false;

    Transform playerPos;

    Vector3 playerxz;

    float grav = 10;

    Vector3 moverVector;

    float yvel;

    float jumpHeight = 2.1f;
    bool jumpTheWall = false;

    bool movePlease = false;

    float maxDistance = 1f;

    //detectionbools
    bool thingfront = false;
    bool thingback = false;
    bool thingright = false;
    bool thingleft = false;


    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CharacterController>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleDetection();
        Gravity();
        Jump();

        TargetPlayer();

        
    }

    void ObstacleDetection()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {

            if (hit.collider.gameObject.tag == "wall")
            {
                Debug.Log("Hit Wall" + Time.deltaTime);
                jumpTheWall = true;
            }
            else
            {
                jumpTheWall = false;
            }

        }
        Debug.DrawRay(transform.position, transform.forward);
    }
            
    
    void TargetPlayer()
    {
        playerxz = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);

        transform.LookAt(playerxz);

        
            moverVector = new Vector3(transform.forward.x, yvel, transform.forward.z);
        
        
               
        if (!closeEnough) 
        {
            speed = 12;
            mover.Move(moverVector * speed * Time.deltaTime);

        }
        else
        {
            if (movePlease)
            {
                speed = -.5f;
                mover.Move(moverVector * speed * Time.deltaTime);
            }
            else
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * 20);
                mover.Move(new Vector3((moverVector.x * speed), yvel * 12, (moverVector.z * speed)) * Time.deltaTime);
            }

        }

    }

    void Gravity()
    {
        if (mover.isGrounded)
        {
            yvel = 0f;
            
        }
        else
        {
          
            yvel -= grav * Time.deltaTime;
            yvel = Mathf.Clamp(yvel, -20f, 20); //max velocity.y )
        }
    }

    void Jump()
    {

        if (mover.isGrounded)
        {
           // Debug.Log("shits"+ Time.deltaTime);
            if (jumpTheWall)//Input.GetButtonDown("Jump") || jumpTheWall)
            {
               
                yvel = jumpHeight;

            }

        }

    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "personalSpace")
        {
            closeEnough = true;
        }
        if (other.tag == "outtatheway")
        {
            movePlease = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "personalSpace")
        {
            closeEnough = false;
        }
        if (other.tag == "outtatheway")
        {
            movePlease = false;
        }
    }

   

}
