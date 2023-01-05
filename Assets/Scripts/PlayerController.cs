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

        //kill a crewmate
        if (Input.GetKey(KeyCode.Space))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward, out hit, 4f))
            {
                
                if (hit.collider.gameObject.TryGetComponent<CrewmateBehaviour>(out CrewmateBehaviour crewmate))
                {
                    Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.green, 1f);
                    crewmate.Kill();
                }
                else{
                    Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.blue, 1f);
                }
            }
            else{
                Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.red, 1f);
            }
        
        }
        }
    }

    public void SetCanMove(bool canmove)
    {
        this.canmove = canmove;
    }

}
