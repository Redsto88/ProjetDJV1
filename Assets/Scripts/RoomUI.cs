using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    public List<Image> _images = new List<Image>();

    public int nombre_a_afficher = 0;

    public int _roomID = 0;

    void Start(){
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            _images.Add(image);
            image.gameObject.SetActive(false);
            nombre_a_afficher = 0;
        }
    }

    void Update(){
        for(int i = 0; i < _images.Count; i++){
            if(i < nombre_a_afficher){
                _images[i].gameObject.SetActive(true);
                Debug.Log("oui : " + i);
            }else{
                _images[i].gameObject.SetActive(false);
            }
        }
    }
}
