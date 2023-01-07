using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
   
    public GameObject _Regles;

    public void Quit(){
         Application.Quit();
    }

    public void Play(){
        SceneManager.LoadScene(1);
    }

    public void Regles(){
        _Regles.SetActive(true);
    }


}
