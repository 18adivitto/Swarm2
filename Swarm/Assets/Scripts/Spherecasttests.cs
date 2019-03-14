using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherecasttests : MonoBehaviour
{

    CharacterController mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        SphereDraw();
        
    }

    void SphereDraw()
    {
        RaycastHit fronthit;
        RaycastHit backhit;
        RaycastHit righthit;
        RaycastHit lefthit;


        if (Physics.SphereCast(transform.position, .5f, transform.forward, out fronthit, 1)) // front 
        {
            if (fronthit.collider != null)
            {
                Debug.Log("Front");
            }
        }
        if (Physics.SphereCast(transform.position, .5f, -transform.forward, out backhit, 1)) // back
        {
            if (backhit.collider != null)
            {
                Debug.Log("Back");
            }
        }
        if (Physics.SphereCast(transform.position, .5f, transform.right, out righthit, 1)) // right 
        {
            if (righthit.collider != null)
            {
                Debug.Log("Right");
            }
        }
        if (Physics.SphereCast(transform.position, .5f, -transform.right, out lefthit, 1)) // left
        {
            if (lefthit.collider != null)
            {
                Debug.Log("Left");
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,.5f);
        Gizmos.DrawSphere(transform.position, 1.5f);
    }
}
