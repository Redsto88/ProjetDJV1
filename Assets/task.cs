using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task : MonoBehaviour
{
    public Vector3 _position;
    void Awake()
    {
        _position = transform.position;
    }
}
