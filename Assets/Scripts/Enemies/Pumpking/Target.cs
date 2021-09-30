using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject[] blood;
    public GameObject explosion;

    int i;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health < 40)
            i = 0;
        if (health < 30)
            i = 1;
        if (health < 20)
            i = 2;
        if (health < 10)
            i = 3;


        blood[i].SetActive(true);

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        explosion.SetActive(true);
        Destroy(gameObject);
    }
}
