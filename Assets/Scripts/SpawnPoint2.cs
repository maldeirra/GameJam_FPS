using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2 : MonoBehaviour
{
    public float spawnDelay = 2f;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}