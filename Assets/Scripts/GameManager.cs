using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[SingletonOptions("GAME", isPrefab: true)]
public class GameManager : Singleton<GameManager>
{


    public int _numberOfRooms = 10;
    public int _startingNumberOfCharacters = 2; //4 crewmates + 1 impostor + 1 player

    public bool _meeting_stop = false;
    public bool toremove = false;

    public int _numberOfCharacters = 2;
    public int _startingNumberOfCrewmates = 1;
    public int _numberOfCrewmates = 1; 
    [SerializeField] private GameObject _crewmatePrefab;
    [SerializeField] private GameObject _imposterPrefab;
    [SerializeField] private GameObject _player;

    public List<Vector3> _tasksPosition = new List<Vector3>();
    
    private List<GameObject> _crewmates = new List<GameObject>();
    public List<GameObject> _imposters = new List<GameObject>();

    public List<GameObject> _characterList = new List<GameObject>();
    

    public List<Material> _characterMaterials = new List<Material>();
    
    public Vector3 _startingPosition = new Vector3(0, 0, 0);
    public float _spawnDistance = 5f;

    public GameObject _camera;

    [SerializeField] private GameObject _meeting;

    [SerializeField] private GameObject _reunionDurgence;

    [SerializeField] private Transform _ejectPos;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getTasks());
        _startingPosition = transform.position;
        for (int i = 0; i < _startingNumberOfCrewmates; i++)
        {
            Vector3 startc = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i)),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i)));
            GameObject crewmate = Instantiate(
                _crewmatePrefab,
                 startc,
                  Quaternion.identity
            );
            crewmate.GetComponentInChildren<Renderer>().material = _characterMaterials[i];
            crewmate.name = "Crewmate " + i;
            crewmate.transform.parent = _player.transform.parent;
            CharacterBehaviour cc = crewmate.GetComponent<CharacterBehaviour>();
            cc._name = _characterMaterials[i].name;
            cc._startPos = startc;
            _crewmates.Add(crewmate);
            _characterList.Add(crewmate);
        }

        for (int i = 0 ; i < _startingNumberOfCharacters - _startingNumberOfCrewmates; i++)
        {
            Vector3 starti = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i+_startingNumberOfCrewmates)),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i+_startingNumberOfCrewmates)));
            GameObject imposter = 
            Instantiate(
                _imposterPrefab,
                 starti,
                  Quaternion.identity
            );
            imposter.GetComponent<CharacterBehaviour>()._isImpostor = true;
            imposter.GetComponentInChildren<Renderer>().material = _characterMaterials[i+_startingNumberOfCrewmates];
            imposter.name = "Imposter " + i; 
            imposter.transform.parent = _player.transform.parent;
            imposter.GetComponent<ImpostorBehaviour>()._player = _player;
            CharacterBehaviour cc = imposter.GetComponent<CharacterBehaviour>();
            cc._name = _characterMaterials[i + _startingNumberOfCrewmates].name;
            cc._startPos = starti;
            _imposters.Add(imposter);
            _characterList.Add(imposter);
        }

        Vector3 start = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI),0,_spawnDistance*Mathf.Sin(2*Mathf.PI));
        _characterList.Add(_player);
        _player.GetComponent<PlayerController>()._startPos = start;
        _player.transform.position = start;
    }

    private void SpawnCrewmate(){

    }



    // Update is called once per frame
    void Update()
    {
        if (_numberOfCrewmates + 1 <= _numberOfCharacters - _numberOfCrewmates) //if the imposters outnumber the crewmates
        {
            Debug.Log("Imposters win!");
        }
        if(_meeting_stop && !toremove){
            toremove = true;
            meeting();
        }
    }

    IEnumerator getTasks()
    {
        yield return new WaitForEndOfFrame();
        task[] tasks = GetComponentsInChildren<task>();
        foreach (task task in tasks)
        {
            _tasksPosition.Add(task._position);
        }
    }

    public void meeting(){
        _meeting_stop = true;
        StartCoroutine(meetingCoroutine());

        IEnumerator meetingCoroutine(){
            _reunionDurgence.SetActive(true);
            yield return new WaitForSeconds(4);
            ResetAllPos(false);
            _reunionDurgence.SetActive(false);
            _meeting.SetActive(true);
            _meeting.GetComponentInChildren<MeetingButtonScript>().Appear();
        }
    }
    


    public void kill(int i){
        GameObject character = _characterList[i];
        GameObject body = null;
        bool lost = false;
        if (character.TryGetComponent<CrewmateBehaviour>(out CrewmateBehaviour crewmate))
        {
            body = crewmate.Kill();
        }
        else if (character.TryGetComponent<ImpostorBehaviour>(out ImpostorBehaviour imposter))
        {
            body = imposter.Kill();
        }
        else
        {
            body = _player.GetComponent<PlayerController>().Kill();
            Debug.Log("Player killed");
            lost = true;
        }
        StartCoroutine(Eject(body));
    }


    IEnumerator Eject(GameObject body)
    {
        _meeting.SetActive(false);
        if(body!=null){
            body.GetComponent<blood>()._blood.SetActive(false);
            body.GetComponent<BodyBehaviour>().Animation();
            body.transform.position = _ejectPos.position;
        }
        _camera.GetComponent<CameraMovement>()._isPlayer = false;
        ResetAllPos(true);
        yield return new WaitForSeconds(5f);
        _meeting_stop = false;
        toremove = false;
        _camera.GetComponent<CameraMovement>()._isPlayer = true;
        foreach(GameObject imposter in _imposters){
            ImpostorBehaviour imposterBehaviour = imposter.GetComponent<ImpostorBehaviour>();
            imposterBehaviour._coolDown = imposterBehaviour._coolDowntime;
        }
        _player.GetComponent<PlayerController>().canmove = true;
        
        
    }
    private void ResetAllPos(bool a){
        foreach (GameObject character in _characterList)
            {
                if(a){
                    if(character.TryGetComponent<CharacterBehaviour>(out CharacterBehaviour characterBehaviour))
                    {
                        characterBehaviour.transform.position = characterBehaviour._startPos;
                    }
                }
                if(character.TryGetComponent<BodyBehaviour>(out BodyBehaviour bodyBehaviour))
                {
                    bodyBehaviour.transform.position = _ejectPos.position;
                }
                else if(character.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    playerController.returnToStart();
                }
            }
    }
}
