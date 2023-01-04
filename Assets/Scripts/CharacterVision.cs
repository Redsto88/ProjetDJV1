using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVision : MonoBehaviour
{
    private List<GameObject> _notVisibleCharacterList;

    // Update is called once per frame
    void Update()
    {
        _notVisibleCharacterList = new List<GameObject>(GameManager.Instance._characterList);
        RaycastHit hit;
        for (int i = 0; i<360; i+=4){
            float cos = Mathf.Cos(i*Mathf.PI/180);
            float sin = Mathf.Sin(i*Mathf.PI/180);
            if (Physics.Raycast(transform.position + new Vector3(cos*0.47f,1.8f,sin*0.47f), new Vector3(cos,0,sin), out hit, 18)){
                if(hit.collider.gameObject.TryGetComponent<CharacterBehaviour>(out CharacterBehaviour character)){
                    _notVisibleCharacterList.Remove(character.gameObject);
                    hit.collider.gameObject.GetComponent<visibilty>()._visible = true;
                    Debug.DrawRay(transform.position + new Vector3(cos*0.47f,1.8f,sin*0.47f), new Vector3(cos,0,sin)*hit.distance, Color.yellow);
                }
                else{
                    Debug.DrawRay(transform.position + new Vector3(cos*0.47f,1.8f,sin*0.47f), new Vector3(cos,0,sin)*hit.distance, Color.red);
                }
            }
            else{
                Debug.DrawRay(transform.position + new Vector3(cos*0.47f,1.8f,sin*0.47f), new Vector3(cos,0,sin)*18, Color.blue);
            }
        }
        foreach (GameObject character in _notVisibleCharacterList){
            character.GetComponent<visibilty>()._visible = false;
        }
        
        
    }
}
