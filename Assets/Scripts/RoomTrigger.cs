using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{


    public List<GameObject> _characters = new List<GameObject>();

    public List<GameObject> _crewmates = new List<GameObject>();

    public RoomUI _roomUI;

    public int _impostorNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterPosition>(out var character)) //if the object that enters the trigger is a character
        {
            _characters.Add(character.gameObject); //add the character to the list of characters in the room
            character._room = this.gameObject; //set the character's room to this room
            if(character.TryGetComponent<CharacterBehaviour>(out var behaviour))
            {
                if(behaviour._isImpostor)
                {
                    _impostorNumber++;
                }
                else{
                    _crewmates.Add(character.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterPosition>(out var character))
        {
            _characters.Remove(character.gameObject); //remove the character from the list of characters in the room
            character.gameObject.GetComponent<CharacterPosition>()._room = null; //set the character's room to null
            if(character.TryGetComponent<CharacterBehaviour>(out var behaviour))
            {
                if(behaviour._isImpostor)
                {
                    _impostorNumber--;
                }
                else{
                    _crewmates.Remove(character.gameObject);
                }
            }
        }
    }
    

}
