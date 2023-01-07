using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegleScript : MonoBehaviour
{
    public void Quit(){
        AudioManager.Instance.Play("selectMenu", 1f);
        gameObject.SetActive(false);
    }
}
