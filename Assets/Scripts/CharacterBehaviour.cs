using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterBehaviour : MonoBehaviour
{



    private float _speed = 6f;
    private NavMeshAgent _agent;

    public bool _isImpostor = false;

    private List<Vector3> _tasks = new List<Vector3>();

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void OnEnable()
    {


        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return null;
            _agent.enabled = true;
            while(enabled)
            {
                _agent.speed = _speed;
                bool random = Random.Range(0, 2) == 0;
                if (random)
                {
                    _agent.SetDestination(new Vector3(
                        Random.Range(-12f, 12f),
                        0f,
                        Random.Range(-12f, 12f)));
                }
                else
                {
                    _agent.SetDestination(new Vector3(
                        Random.Range(-12f, 12f) + 40,
                        0f,
                        Random.Range(-12f, 12f)));
                }
                do
                {
                    yield return null;
                }
                while (_agent.hasPath);


                yield return new WaitForSeconds(3);
            }
            
            
        }
    }
}
