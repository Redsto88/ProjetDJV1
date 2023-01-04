using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private bool canmove = true;
    private CharacterController _controller;
    
    public float _speed = 2f;
    public float _rotationSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(canmove){
        // Get the input from the player
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Move the player
        _controller.Move(move * Time.deltaTime * _speed);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); // Keep the player on the ground

        // Rotate the player
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), _rotationSpeed * Time.deltaTime);
        }
        
        }
    }

    public void SetCanMove(bool canmove)
    {
        this.canmove = canmove;
    }

}
