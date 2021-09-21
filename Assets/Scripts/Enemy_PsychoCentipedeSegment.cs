using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PsychoCentipedeSegment : MonoBehaviour
{

    public float previousDist = 1f;
    public GameObject previous = null;

    float rotSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ProgressiveLookAt();
        ProgressiveMove();
    }

    void ProgressiveLookAt()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = previous.transform.position - transform.position;
        targetDirection.Normalize();

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotSpeed * Time.deltaTime, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ProgressiveMove()
    {
        Vector3 targetPos = previous.transform.position - previous.transform.forward * previousDist;
        Vector3 v = targetPos - transform.position;

        transform.position += 2 * v * Time.deltaTime;
    }
}
