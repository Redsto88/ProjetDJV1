using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
   
    public GameObject _Regles;

    public void Quit(){
        AudioManager.Instance.Play("selectMenu", 1f);
         Application.Quit();
    }

    public void Play(){
        AudioManager.Instance.Play("selectMenu", 1f);
        SceneManager.LoadScene(1);
    }

    public void Regles(){
        _Regles.SetActive(true);
        AudioManager.Instance.Play("selectMenu", 1f);
    }

    void Start()
    {
        AudioManager.Instance.Play("menu", 0.5f);
    }


}
