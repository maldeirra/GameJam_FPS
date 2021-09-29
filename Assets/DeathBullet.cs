using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBullet : MonoBehaviour
{
    public float bulletMaxLifeTime = 1f;
    public float damages = 1f;
    public GameObject impact;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletMaxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        damageable?.TakeDamage(damages);

        GameObject newImpact = Instantiate(impact, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
