using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    public GameObject explosion;
    public float waitTime = 5f;


    void Start()
    {
        StartCoroutine(DestructionBullet(waitTime));
    }

    IEnumerator DestructionBullet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.transform);

        if (collision.gameObject.tag == "Ennemi")
        {
            collision.gameObject.GetComponent<Target>().TakeDamage(5f);
        }
    }
}
