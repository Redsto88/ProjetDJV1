using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cielScript : MonoBehaviour
{

    Material m;
    
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        m.SetTextureOffset("_MainTex", new Vector2(-Time.time * speed, 0));
    }
}
