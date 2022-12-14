using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehaviour : MonoBehaviour
{

    private bool _isActivated = false;

    public string _name = "Player";

    [SerializeField] private GameObject _outline;

    void OnTriggerStay(Collider other){
        if(other.gameObject.TryGetComponent<PlayerController>(out var c)){
            if(Input.GetKey(KeyCode.Space) && !_isActivated){
                _isActivated = true;
                GameManager.Instance.meeting(false);
            }
        }
    }

    public void Animation(){
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            float t = 0;
            while(t<7){
                t+=Time.deltaTime;
                transform.position = new Vector3(transform.position.x+0.04f, transform.position.y, transform.position.z);
                transform.rotation *= Quaternion.Euler(0, 0, 1f);
                yield return null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player) && !_isActivated)
        {
            _outline.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            _outline.SetActive(false);
        }
    }
}
