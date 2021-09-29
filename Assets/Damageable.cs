using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public GameObject explosion;
    public float healph = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
            Explode();
    }

    public void TakeDamage(float damage)
    {
        healph -= damage;
    }

    bool IsDead()
    {
        return healph <= 0f;
    }

    public void Explode()
    {
        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
