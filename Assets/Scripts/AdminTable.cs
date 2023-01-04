using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminTable : MonoBehaviour
{
    public bool active = false;
    public bool state = false;

    public GameObject table;



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
                table.SetActive(true);
                player.SetCanMove(false);
                
            }
            else if (Input.GetKey(KeyCode.Space) && !active && state)
            {
                active = true;
                state = false;
                table.SetActive(false);
                player.SetCanMove(true);
            }
        }
    }
}
