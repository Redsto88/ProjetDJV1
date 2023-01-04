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

        if (other.TryGetComponent(out PlayerController player)){
            if(!Input.GetKey(KeyCode.Space)){
                active = false;
            }


            if (Input.GetKey(KeyCode.Space) && !active && !state)
            {
                active = true;
                state = true;
                other.GetComponent<CharacterVision>()._camera = true;
                cameras.SetActive(true);
                player.SetCanMove(false);
                
            }
            else if (Input.GetKey(KeyCode.Space) && !active && state)
            {
                active = true;
                state = false;
                other.GetComponent<CharacterVision>()._camera = false;
                cameras.SetActive(false);
                player.SetCanMove(true);
            }
        }
    }
}
