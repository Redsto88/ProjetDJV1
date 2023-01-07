using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminTable : MonoBehaviour
{
    public bool active = true;
    public bool state = false;

    public GameObject table;

    public GameObject rooms;

    private RoomTrigger[] roomsT = new RoomTrigger[0];

    [SerializeField] private GameObject outline;

    void Update(){
        if (state)
        {
            roomsT =  rooms.GetComponentsInChildren<RoomTrigger>();
            foreach (RoomTrigger room in roomsT)
            {
                room._roomUI.nombre_a_afficher = room._characters.Count;
            }
        }
    }


    void OnTriggerStay(Collider other)
    {

        if (other.TryGetComponent(out PlayerController player)){
            if(!Input.GetKey(KeyCode.Space)){
                active = false;
            }


            if (Input.GetKey(KeyCode.Space) && !active && !state)
            {
                active = true;
                state = true;
                table.SetActive(true);
                player.SetCanMove(false);
                
            }
            else if (Input.GetKey(KeyCode.Space) && !active && state)
            {
                active = true;
                state = false;
                table.SetActive(false);
                player.SetCanMove(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            outline.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            outline.SetActive(false);
        }
    }
}
