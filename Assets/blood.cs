using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{

    public GameObject _blood;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBlood());
    }

    IEnumerator spawnBlood()
    {
            GameObject _bloodSpawned = Instantiate(_blood, transform.position, Quaternion.identity);
            Destroy(_bloodSpawned,2);
            yield return null;
    }
}