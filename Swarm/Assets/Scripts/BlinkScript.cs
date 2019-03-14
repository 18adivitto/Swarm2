using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    MeshRenderer eyelid;
    
    // Start is called before the first frame update
    void Start()
    {
        eyelid = GetComponent<MeshRenderer>();
        eyelid.enabled = false;
        StartCoroutine(blinker());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator blinker()
    {
        yield return new WaitForSeconds(Random.Range(2.0f,5.0f));
        eyelid.enabled = true;
        yield return new WaitForSeconds(.1f);
        eyelid.enabled = false;
        StartCoroutine(blinker());
    }
}
