using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilty : MonoBehaviour
{
    public bool _visible = true;

    private bool _lastVisible = true;
    private Renderer[] rends;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (_visible != _lastVisible)
        {
            render(_visible);
            _lastVisible = _visible;
        }
    }

    void render(bool a){
        foreach (Renderer rend in rends)
        {
            rend.enabled = a;
        }
    }
}
