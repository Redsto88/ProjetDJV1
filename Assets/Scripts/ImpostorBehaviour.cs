using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ImpostorBehaviour : CharacterBehaviour
{



    private float _speed = 6f;
    private NavMeshAgent _agent;

    private List<Vector3> _tasks = new List<Vector3>();

    private Vector3 _starting_position;

    public bool _isStopped = false;

    public GameObject _player;

    public GameObject _target;

    public float _coolDowntime = 45;

    public float _coolDown = 20; //start with 20 seconds cooldown

    // Start is called before the first frame update
    void Awake()
    {
        _starting_position = transform.position;
        _agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (GameManager.Instance.stop){
            _agent.isStopped = true;
        }
        if (_coolDown > 0){
            _coolDown -= Time.deltaTime;
        }
        else{
            _coolDown = 0;
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
                do{
                    if(Vector3.Distance(transform.position,_destination)<=1){
                        _agent.isStopped = true;
                    }
                    if (_target == null && _coolDown == 0){
                        GameObject _roomObject = GetComponent<CharacterPosition>()._room;
                        if(_roomObject != null){
                            RoomTrigger _room = _roomObject.GetComponent<RoomTrigger>();
                            if (_room._impostorNumber >= _room._characters.Count - _room._impostorNumber && Vector3.Distance(transform.position,_player.transform.position)>10 && _room._crewmates.Count>0){
                                foreach (GameObject crew in _room._crewmates)
                                {
                                    if(crew != null && crew.TryGetComponent<CrewmateBehaviour>(out var crewmate)){;
                                        if (!crewmate._isTargetted){
                                            _target = crew;
                                            crewmate._isTargetted = true;
                                            _agent.SetDestination(crew.transform.position);
                                            _destination = crew.transform.position;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    yield return null;
                }
                while (enabled && !_agent.isStopped);
                if (_target != null){
                    CrewmateBehaviour crewmate = _target.GetComponent<CrewmateBehaviour>();
                    crewmate.Kill();
                    _coolDown = _coolDowntime;
                    _target = null;
                }
                else{
                    yield return new WaitForSeconds(5);
                }
                _agent.isStopped = false;
            }
        }
    }
}
