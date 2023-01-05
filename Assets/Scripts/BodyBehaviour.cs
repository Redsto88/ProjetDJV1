using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehaviour : MonoBehaviour
{

    private bool _isActivated = false;

    public string _name = "Player";

    void OnTriggerStay(){

        if(Input.GetKey(KeyCode.Space) && !_isActivated){
            _isActivated = true;
        }
    }
}
