using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyButton : MonoBehaviour
{

    [SerializeField] private GameObject _buttonOutline;
    private bool _isActivated = false;

    public float cooldownTime = 60f;
    [SerializeField] private float cooldownTimer = 0f;

    void Update(){
        if(_isActivated && !GameManager.Instance._meeting_stop){
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= cooldownTime){
                _isActivated = false;
                cooldownTimer = 0f;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player) && !_isActivated)
        {
            _buttonOutline.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            _buttonOutline.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            if(Input.GetKey(KeyCode.Space) && !_isActivated){
                _buttonOutline.SetActive(false);
                _isActivated = true;
                GameManager.Instance.meeting();
            }
        }
    }


}
