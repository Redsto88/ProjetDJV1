using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starBackMain : MonoBehaviour
{

    public GameObject _star;

    public int max = 200;

    void Start()
    {
        
        //spawn stars randomly on the background
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-max, max) + transform.position.x, transform.position.y, Random.Range(-0.75f*max, 0.75f*max)+ transform.position.z);
            GameObject starspawn = Instantiate(_star, pos, transform.rotation);
            starspawn.GetComponent<starMain>()._size = Random.Range(1, 4);
            starspawn.GetComponent<starMain>()._max = max;
            starspawn.transform.SetParent(transform);
        }
    }

    void Update()
    {
        
    }
}
