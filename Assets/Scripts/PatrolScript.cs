using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public GameObject[] ePosition;
    public float  speed = 10f;

    int i = 0;

    void Start() 
    {
        transform.position = Vector3.MoveTowards(transform.position, ePosition[i].transform.position, speed*Time.deltaTime);
    }

    void Update() 
    {

        if (transform.position == ePosition[i].transform.position)
        {
            i++;

            if (i >= ePosition.Length)
            {
                i = 0;
            }
        }

        transform.LookAt(ePosition[i].transform);
        transform.position = Vector3.MoveTowards(transform.position, ePosition[i].transform.position, speed*Time.deltaTime);
    }
}
