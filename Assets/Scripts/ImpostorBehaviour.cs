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

    // Start is called before the first frame update
    void Awake()
    {
        _starting_position = transform.position;
        _agent = GetComponent<NavMeshAgent>();
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
                    if(Vector3.Distance(transform.position,_destination)<=3){
                        _agent.ResetPath();
                    }
                    yield return null;
                }
                while (_agent.hasPath);

                yield return new WaitForSeconds(5);
            }
            
            
        }
    }
}
