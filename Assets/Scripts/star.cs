using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour
{

    public int _size = 0;

    public int _max = 10;

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(_size, _size);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(((float) _size)/8, 0, 0);
        if (transform.position.x > _max)
        {
            transform.position = new Vector3(0, transform.position.y, 0);
        }
    }
}
