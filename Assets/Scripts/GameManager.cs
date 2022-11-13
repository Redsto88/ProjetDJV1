using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public int _numberOfRooms = 10;
    public int _startingNumberOfCharacters = 6; //4 crewmates + 1 impostor + 1 player

    public int _numberOfCharacters = 6;
    public int _startingNumberOfCrewmates = 4;
    public int _numberOfCrewmates = 4; 
    [SerializeField] private GameObject _crewmatePrefab;
    [SerializeField] private GameObject _imposterPrefab;
    [SerializeField] private GameObject _player;
    
    private List<GameObject> _crewmates = new List<GameObject>();
    private List<GameObject> _imposters = new List<GameObject>();
    

    public List<Material> _characterMaterials = new List<Material>();
    
    public Vector3 _startingPosition = new Vector3(0, 0, 0);
    public float _spawnDistance = 5f;

    





    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _startingNumberOfCrewmates; i++)
        {
            GameObject crewmate = Instantiate(
                _crewmatePrefab,
                 _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/_startingNumberOfCharacters*i),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/_startingNumberOfCharacters*i)),
                  Quaternion.identity
            );
            crewmate.GetComponentInChildren<Renderer>().material = _characterMaterials[i];
            crewmate.name = "Crewmate " + i;
            crewmate.transform.parent = _player.transform.parent;
            _crewmates.Add(crewmate);
        }

        for (int i = 0 ; i < _startingNumberOfCharacters - _startingNumberOfCrewmates; i++)
        {
            GameObject imposter = Instantiate(
                _imposterPrefab,
                 _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/_startingNumberOfCharacters*(i+_startingNumberOfCrewmates)),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/_startingNumberOfCharacters*(i+_startingNumberOfCrewmates))),
                  Quaternion.identity
            );
            imposter.GetComponent<CharacterBehaviour>()._isImpostor = true;
            imposter.GetComponentInChildren<Renderer>().material = _characterMaterials[i+_startingNumberOfCrewmates];
            imposter.name = "Imposter " + i; 
            imposter.transform.parent = _player.transform.parent;
            _imposters.Add(imposter);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_numberOfCrewmates <= _numberOfCharacters - _numberOfCrewmates) //if the imposters outnumber the crewmates
        {
            Debug.Log("Imposters win!");
        }
    }
}
