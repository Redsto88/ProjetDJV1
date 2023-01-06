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

    [SerializeField] private GameObject _Rooms;

    [SerializeField] private GameObject _meeting;

    [SerializeField] private GameObject _reunionDurgence;

    [SerializeField] private Transform _ejectPos;

    [SerializeField] private GameObject _ejectText;


    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        int crewmateToSpawn = _startingNumberOfCrewmates;
        int impostorToSpawn = _startingNumberOfCharacters - _startingNumberOfCrewmates;
        int playerToSpawnN = 1;

        for(int i = 0; i< _numberOfCharacters+1; i++){
            bool good = false;
            while(!good){
                int random = Random.Range(0,12);
                if (random<10){
                    if (crewmateToSpawn > 0){
                        crewmateToSpawn -= 1;
                        SpawnCrewmate(i,playerToSpawnN==1 ? i : i-1);
                        good = true;
                    }
                }
                else if (random==10){
                    if (playerToSpawnN > 0){
                        playerToSpawnN -= 1;
                        SpawnPlayer(i);
                        good = true;
                    }
                }
                if (random==2){
                    if (impostorToSpawn > 0){
                        impostorToSpawn -= 1;
                        SpawnImpostor(i,playerToSpawnN==1 ? i : i-1);
                        good = true;
                    }
                }
            }

        }


        StartCoroutine(getTasks());
    }

    private void SpawnCrewmate(int i,int mat){
        Vector3 start = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i)),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/(_startingNumberOfCharacters+1)*(i)));
            GameObject crewmate = Instantiate(
                _crewmatePrefab,
                 start,
                  Quaternion.identity
            );
            crewmate.GetComponentInChildren<Renderer>().material = _characterMaterials[mat];
            crewmate.name = "Crewmate " + i;
            crewmate.transform.parent = _player.transform.parent;
            CharacterBehaviour cc = crewmate.GetComponent<CharacterBehaviour>();
            cc._name = _characterMaterials[mat].name;
            cc._startPos = start;
            _crewmates.Add(crewmate);
            _characterList.Add(crewmate);
    }

    private void SpawnImpostor(int i,int mat){
        Vector3 start = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/(_startingNumberOfCharacters+1)*i),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/(_startingNumberOfCharacters+1)*i));
            GameObject imposter = 
            Instantiate(
                _imposterPrefab,
                 start,
                  Quaternion.identity
            );
            imposter.GetComponent<CharacterBehaviour>()._isImpostor = true;
            imposter.GetComponentInChildren<Renderer>().material = _characterMaterials[mat];
            imposter.name = "Imposter " + i; 
            imposter.transform.parent = _player.transform.parent;
            imposter.GetComponent<ImpostorBehaviour>()._player = _player;
            CharacterBehaviour cc = imposter.GetComponent<CharacterBehaviour>();
            cc._name = _characterMaterials[mat].name;
            cc._startPos = start;
            _imposters.Add(imposter);
            _characterList.Add(imposter);
    }

    private void SpawnPlayer(int i){
         Vector3 start = _startingPosition + new Vector3(_spawnDistance*Mathf.Cos(2*Mathf.PI/(_startingNumberOfCharacters+1)*i),0,_spawnDistance*Mathf.Sin(2*Mathf.PI/(_startingNumberOfCharacters+1)*i));
        _characterList.Add(_player);
        _player.GetComponent<PlayerController>()._startPos = start;
        _player.transform.position = start;
    }
    private void resetRoomsPresence(){
        foreach (Transform child in _Rooms.transform)
        {
            List<GameObject> chars = new List<GameObject>(child.GetComponent<RoomTrigger>()._characters);
            foreach (GameObject character in chars)
            {
                if (character==null){
                    child.gameObject.GetComponent<RoomTrigger>()._characters.Remove(character);
                }
                else if (character.TryGetComponent<BodyBehaviour>(out BodyBehaviour body))
                {
                    child.gameObject.GetComponent<RoomTrigger>()._characters.Remove(character);
                }
            }
        }
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
    


    public void kill(int i,string name){
        GameObject character = _characterList[i];
        GameObject body = null;
        int who = -1;
        bool lost = false;
        if (character.TryGetComponent<CrewmateBehaviour>(out CrewmateBehaviour crewmate))
        {
            body = crewmate.Kill();
            who = 0;
        }
        else if (character.TryGetComponent<ImpostorBehaviour>(out ImpostorBehaviour imposter))
        {
            body = imposter.Kill();
            who = 1;
        }
        else
        {
            body = _player.GetComponent<PlayerController>().Kill();
            who = 2;
            Debug.Log("Player killed");
            lost = true;
        }
        StartCoroutine(Eject(body,who,name));
    }


    IEnumerator Eject(GameObject body,int who, string name)
    {
        if(body.TryGetComponent<visibilty>(out visibilty v)){
            v._isEjected = true;
        }
        _ejectText.SetActive(true);
        if (who == 0)
        {
            _ejectText.GetComponent<WasImpostor>()._textToPrint = name + " n'etait pas imposteur";
        }
        else if (who == 1)
        {
            _ejectText.GetComponent<WasImpostor>()._textToPrint = name + " etait imposteur";
        }
        else
        {
            _ejectText.GetComponent<WasImpostor>()._textToPrint = " Vous vous etes tue";
        }
        _meeting.SetActive(false);
        if(body!=null){
            body.GetComponent<blood>()._blood.SetActive(false);
            body.GetComponent<BodyBehaviour>().Animation();
            body.transform.position = _ejectPos.position;
        }
        _camera.GetComponent<CameraMovement>()._isPlayer = false;
        ResetAllPos(true);
        yield return new WaitForSeconds(7f);
        _ejectText.SetActive(false);
        _meeting_stop = false;
        toremove = false;
        _camera.GetComponent<CameraMovement>()._isPlayer = true;
        foreach(GameObject imposter in _imposters){
            ImpostorBehaviour imposterBehaviour = imposter.GetComponent<ImpostorBehaviour>();
            imposterBehaviour._coolDown = imposterBehaviour._coolDowntime;
        }
        _player.GetComponent<PlayerController>().canmove = true;
        resetRoomsPresence();

        
        
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
