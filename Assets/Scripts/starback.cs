using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starback : MonoBehaviour
{

    public GameObject _star;

    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        //spawn stars randomly on the background
        for (int i = 0; i < 100; i++)
        {
            int width = (int)_rectTransform.rect.width;
            int height = (int)_rectTransform.rect.height;
            Vector3 pos = new Vector3(Random.Range(0, width), Random.Range(0, height), 0);
            GameObject starspawn = Instantiate(_star, pos, transform.rotation);
            starspawn.GetComponent<star>()._size = Random.Range(1, 10);
            starspawn.GetComponent<star>()._max = width;
            starspawn.transform.SetParent(transform);
        }
    }

    void Update()
    {
        
    }
}
