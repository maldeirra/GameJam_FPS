using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PsychoCentipedeSegment : MonoBehaviour
{

    public int segNum = 0;
    public float segPhi = 0.01f;
    public float previousDist = 1f;
    public GameObject previous = null;

    float oscilloAmp = 0.5f;
    float freq = 2f;
    float rotSpeed = 1.5f;
    Vector3 globalZ = new Vector3(0f, 1f, 0f);

    const float RIGID_FACTOR = 1f;
    const float ELASTIC_FACTOR = 20f;

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
        float oscilloAng = 2f * Mathf.PI * freq * (Time.time + segPhi * segNum);
        //float oscilloAng = 2f * Mathf.PI * freq * Time.time;
        //Vector3 oscillo = transform.up * oscilloAmp * Mathf.Sin(oscilloAng); ;
        float oscillo = oscilloAmp * Mathf.Sin(oscilloAng); ;

        //Vector3 targetPos = previous.transform.position - previous.transform.forward * previousDist + oscillo;
        Vector3 targetPos = previous.transform.position - previous.transform.forward * previousDist + previous.transform.up * oscillo;
        Vector3 v = targetPos - transform.position;
        float norm = v.magnitude;
        float coeff = RIGID_FACTOR + ELASTIC_FACTOR * norm;
        v.Normalize();

        if (!float.IsNaN(coeff) && !(coeff == float.PositiveInfinity))
            transform.position += coeff * v * Time.deltaTime;

        //transform.position += previous.transform.up * oscillo * Time.deltaTime;
    }
}
