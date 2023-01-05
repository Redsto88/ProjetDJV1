using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVision : MonoBehaviour
{
    private List<GameObject> _notVisibleCharacterList;

    public float _visionDistance = 6f;

    public bool _camera = false;

    public float rayHeight = 1.8f;

    public bool DebugMode = false;

    // Update is called once per frame
    void Update()
    {
        _notVisibleCharacterList = new List<GameObject>(GameManager.Instance._characterList);
        RaycastHit hit;
        RaycastHit hit2;
        for (int i = 0; i<360; i+=2){
            float cos = Mathf.Cos(i*Mathf.PI/180);
            float sin = Mathf.Sin(i*Mathf.PI/180);
            if (Physics.Raycast(transform.position + new Vector3(cos*0.47f,rayHeight,sin*0.47f), new Vector3(cos,0,sin), out hit, _visionDistance)){
                if(hit.collider.gameObject.TryGetComponent<visibilty>(out visibilty v)){
                    if(Physics.Raycast(hit.point + new Vector3(cos*0.47f,0,sin*0.47f), new Vector3(cos,0,sin), out hit2, _visionDistance - hit.distance)){
                        if(hit2.collider.gameObject.TryGetComponent<visibilty>(out visibilty v2))
                        {
                            _notVisibleCharacterList.Remove(v2.gameObject);
                            hit2.collider.gameObject.GetComponent<visibilty>()._visible = true;
                            if(DebugMode) Debug.DrawRay(hit.point + new Vector3(cos*0.47f,0,sin*0.47f), new Vector3(cos,0,sin)*(_visionDistance - hit.distance - hit2.distance), Color.green);
                        }
                        else{
                            if(DebugMode) Debug.DrawRay(hit.point + new Vector3(cos*0.47f,0,sin*0.47f), new Vector3(cos,0,sin)*(_visionDistance - hit.distance - hit2.distance), Color.magenta);
                        }
                    }
                    else{
                        if(DebugMode) Debug.DrawRay(hit.point + new Vector3(cos*0.47f,0,sin*0.47f), new Vector3(cos,0,sin)*(_visionDistance - hit.distance), Color.cyan);
                    }
                    _notVisibleCharacterList.Remove(v.gameObject);
                    hit.collider.gameObject.GetComponent<visibilty>()._visible = true;
                    if(DebugMode) Debug.DrawRay(transform.position + new Vector3(cos*0.47f,rayHeight,sin*0.47f), new Vector3(cos,0,sin)*hit.distance, Color.yellow);
                }
                else{
                    if(DebugMode) Debug.DrawRay(transform.position + new Vector3(cos*0.47f,rayHeight,sin*0.47f), new Vector3(cos,0,sin)*hit.distance, Color.red);
                }
            }
            else{
                if(DebugMode) Debug.DrawRay(transform.position + new Vector3(cos*0.47f,rayHeight,sin*0.47f), new Vector3(cos,0,sin)*_visionDistance, Color.blue);
            }
        }
        foreach (GameObject character in _notVisibleCharacterList){
            character.GetComponent<visibilty>()._visible = false || _camera;
        }
        
        
    }
}
