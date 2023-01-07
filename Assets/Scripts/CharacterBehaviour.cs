using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public bool _isImpostor = false;

    public string _name = "Player";

    public Vector3 _startPos;

    public void Kill(){
        if(_isImpostor){
        }
        else{
        }
    }

    public void returnToStart(){
        transform.position = _startPos;
    }
}
