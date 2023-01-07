using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingButtonScript : MonoBehaviour
{
    public List<ButtonScript> _buttons = new List<ButtonScript>();
    void OnEnable()
    {
        ButtonScript[] buttons = GetComponentsInChildren<ButtonScript>();
        foreach (ButtonScript button in buttons)
        {
            _buttons.Add(button);
            button.ParentButton = gameObject;
        }
    }

    public void Skip()
    {
        AudioManager.Instance.Play("valide", 0.7f);
        Valide(-1);
    }

    public void Reset(){
        foreach (ButtonScript button in _buttons)
        {
            button.UnClick();
        }
    }

    public void Valide(int id){
        foreach (ButtonScript button in _buttons)
        {
            button.Unactive();
        }
        StartCoroutine(End(id));

        IEnumerator End(int id){
            yield return new WaitForSeconds(1.5f);
            if (id != -1) GameManager.Instance.kill(id, _buttons[id]._text.text);
            else GameManager.Instance.kill(-1,"Personne");
        }
    }

    public void Appear(){
        int i = 0;
        foreach (GameObject character in GameManager.Instance._characterList)
        {
            if(character.gameObject.TryGetComponent<CharacterBehaviour>(out var a)){
                _buttons[i].SetText(a._name);
                _buttons[i]._id = i;
                _buttons[i].Active();
            }
            if(character.gameObject.TryGetComponent<BodyBehaviour>(out var b)){
                _buttons[i].SetText(b._name);
                _buttons[i].Dead();
                _buttons[i]._id = i;
            }
            if(character.gameObject.TryGetComponent<PlayerController>(out var c)){
                _buttons[i].SetText("You");
                _buttons[i]._id = i;
                _buttons[i].Active();
            }
            i++;
        }
    }
}
