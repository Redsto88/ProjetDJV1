using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    
    public GameObject _Yesbutton;
    public GameObject _Nobutton;
    public GameObject ParentButton;
    public GameObject valide;
    public GameObject _button;

    public Image _background;

    public TextMeshProUGUI _text;

    public int _id;


    public void Click(){
        ParentButton.GetComponent<MeetingButtonScript>().Reset();
        StartCoroutine(Appear());
    }

    IEnumerator Appear(){
        _Yesbutton.SetActive(true);
        _Nobutton.SetActive(true);
        RectTransform Yes = _Yesbutton.GetComponent<RectTransform>();
        RectTransform No = _Nobutton.GetComponent<RectTransform>();
        Yes.localScale = new Vector3(1,1,1);
        No.localScale = new Vector3(1,1,1);
        Yes.localScale = new Vector3(1,0,1);
        No.localScale = new Vector3(1,0,1);
        while(No.localScale.y < 1){
            Yes.localScale = new Vector3(1,Yes.localScale.y + 0.1f,1);
            No.localScale = new Vector3(1,No.localScale.y + 0.1f,1);
            yield return null;
        }
    }

    public void UnClick(){
        _Yesbutton.SetActive(false);
        _Nobutton.SetActive(false);
    }

    public void Yes(){
        valide.SetActive(true);
        ParentButton.GetComponent<MeetingButtonScript>().Valide(_id);
    }

    public void Unactive(){
        _button.SetActive(false);
    }

    public void Active(){
        _button.SetActive(true);
        valide.SetActive(false);
        _Yesbutton.SetActive(false);
        _Nobutton.SetActive(false);

    }

    public void SetText(string text){
        _text.text = text;
    }

    public void Dead(){
        _button.SetActive(false);
        _background.color = new Color(0.5f,0.5f,0.5f,1);
    }

}
