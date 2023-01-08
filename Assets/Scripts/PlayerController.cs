using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public bool canmove = true;
    private CharacterController _controller;
    
    public float _speed = 2f;
    public float _rotationSpeed = 2f;
    
    public GameObject _body;

    public Vector3 _startPos;
    private bool soundCoroutineStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._meeting_stop){
            canmove = false;
        }
        if(canmove){
        // Get the input from the player
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Move the player
        _controller.Move(move * Time.deltaTime * _speed);
        if (move == Vector3.zero && soundCoroutineStarted)
        {
            soundCoroutineStarted = false;
            StopCoroutine("StepSound");
            
        }
        else if (move != Vector3.zero && !soundCoroutineStarted)
        {
            soundCoroutineStarted = true;
            StartCoroutine("StepSound");
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); // Keep the player on the ground

        // Rotate the player
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), _rotationSpeed * Time.deltaTime);
        }

        //kill a crewmate DEBUG ONLY
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     RaycastHit hit;
            
        //     if (Physics.Raycast(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward, out hit, 4f))
        //     {
                
        //         if (hit.collider.gameObject.TryGetComponent<CrewmateBehaviour>(out CrewmateBehaviour crewmate))
        //         {
        //             Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.green, 1f);
        //             crewmate.Kill();
        //         }
        //         else{
        //             Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.blue, 1f);
        //         }
        //     }
        //     else{
        //         Debug.DrawRay(new Vector3(transform.position.x,1.8f,transform.position.z), transform.forward * 4f, Color.red, 1f);
        //     }
        
        // }
        }
        if(!canmove){
            soundCoroutineStarted = false;
            StopCoroutine("StepSound");
        }
    }

    IEnumerator StepSound(){
        while(soundCoroutineStarted){
            int rand = Random.Range(1, 5);
            AudioManager.Instance.Play("step"+rand, 0.5f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void SetCanMove(bool canmove)
    {
        this.canmove = canmove;
    }

    public void returnToStart(){
        transform.position = _startPos;
    }

    public GameObject Kill(){
        GameObject body = Instantiate(_body, transform.position, Quaternion.Euler(-90,0,0));
        body.GetComponentsInChildren<Renderer>()[0].material = GetComponentInChildren<Renderer>().material;
        body.GetComponentsInChildren<Renderer>()[1].material = GetComponentInChildren<Renderer>().material;
        GameManager.Instance._characterList.Add(body);
        return body;
    }

}
