using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public float maxDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxDuration);

        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp, maxDuration);

        AudioSource audioSrc = gameObject.GetComponent<AudioSource>();
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
