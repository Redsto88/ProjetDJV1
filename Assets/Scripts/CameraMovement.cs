using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Vector3 _currentVelocity;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref _currentVelocity,
            0.1f);

        
    }
}
