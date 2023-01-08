using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CrewmateBehaviour : CharacterBehaviour
{



    private float _speed = 6f;
    private NavMeshAgent _agent;

    private List<Vector3> _tasks = new List<Vector3>();

    private Vector3 _starting_position;

    [SerializeField] private GameObject Body;

    public bool _isTargetted = false;

    public bool _isStopped = false;

    private bool _wasTargetted = false;

    public float dist;

    // Start is called before the first frame update
    void Awake()
    {
        _starting_position = transform.position;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameManager.Instance._meeting_stop){
            _agent.SetDestination(_starting_position);
        }
        if (_isTargetted) {
            _agent.isStopped = true;
            _wasTargetted = true;
        }

        if (_wasTargetted && !_isTargetted) {
            _agent.isStopped = false;
            _wasTargetted = false;
        }

    }
    

    // Update is called once per frame
    void OnEnable()
    {


        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(2);
            _agent.enabled = true;
            _agent.speed = _speed;
            while(enabled)
            {
                Vector3 _destination = GameManager.Instance._tasksPosition[Random.Range(0, GameManager.Instance._tasksPosition.Count)];
                _agent.SetDestination(_destination);
                do
                {
                    _destination = _agent.destination;
                    dist = Vector3.Distance(transform.position,_destination);
                    if(Vector3.Distance(transform.position,_destination)<=1){
                        _isStopped = true;
                    }
                    yield return null;
                }
                while (enabled && !_isStopped);
                _isStopped = true;
                yield return new WaitForSeconds(5);
                _isStopped = false;
            }
        }
    }

    new public GameObject Kill(){
        GameObject bodySpawn = Instantiate(Body, transform.position, Quaternion.Euler(-90,0,0));
        bodySpawn.GetComponentsInChildren<Renderer>()[0].material = GetComponentInChildren<Renderer>().material;
        bodySpawn.GetComponentsInChildren<Renderer>()[1].material = GetComponentInChildren<Renderer>().material;
        bodySpawn.GetComponent<BodyBehaviour>()._name = _name;
        GameManager.Instance._characterList.Add(bodySpawn);
        GameManager.Instance._characterList.Remove(gameObject);
        CharacterPosition room = GetComponent<CharacterPosition>();
        if(room != null){
            if (room._room != null){
                RoomTrigger roomObj = room._room.GetComponent<RoomTrigger>();
                roomObj._crewmates.Remove(gameObject);
                roomObj._characters.Remove(gameObject);
                roomObj._characters.Add(bodySpawn);
            }
        }
        GameManager.Instance._numberOfCrewmates--;
        GameManager.Instance._numberOfCharacters--;
        Destroy(gameObject);
        return bodySpawn;
    }
}
