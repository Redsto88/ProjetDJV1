using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public bool active = false;
    public bool state = false;

    public GameObject cameras;



    void OnTriggerStay(Collider other)
    {
        if(!Input.GetKey(KeyCode.Space)){
            active = false;
        }


        if (Input.GetKey(KeyCode.Space) && !active && !state)
        {
            active = true;
            state = true;
            cameras.SetActive(true);
            
        }
        else if (Input.GetKey(KeyCode.Space) && !active && state)
        {
            active = true;
            state = false;
            cameras.SetActive(false);
        }
    }
}
