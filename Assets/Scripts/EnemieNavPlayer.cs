using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemieNavPlayer : MonoBehaviour
{
    //Assign in inspector the target that you want to follow
    public Transform target;

    void Update()
    {
        //Move towards target every frame and stay in the NavMesh
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
