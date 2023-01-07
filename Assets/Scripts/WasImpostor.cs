using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WasImpostor : MonoBehaviour
{

    public TextMeshProUGUI _text;
    public TextMeshProUGUI _text2;

    public string _textToPrint = "You were the impostor";


    void OnEnable()
    {
       
        StartCoroutine(Coroutine());
        
        IEnumerator Coroutine()
        {
             _text2.gameObject.SetActive(false);
            float t = 0;
            AudioManager.Instance.Play("writeText", 1f);
            while(t<_textToPrint.Length){
                t+=Time.deltaTime*6;
                _text.text = _textToPrint.Substring(0, (int) t);
                yield return null;
            }
            AudioManager.Instance.Stop("writeText");
            _text2.gameObject.SetActive(true);
            _text2.text = GameManager.Instance._imposters.Count + " imposteurs restants";
        }
    }
}
