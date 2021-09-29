using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PsychoCentipedeHead : MonoBehaviour
{

    public float rotSpeed = 20f;
    public float force = 200f;
    public float nextSegDistance = 1f;
    public int nSeg = 10;
    public GameObject prefab = null;
    public GameObject spawn = null;

    float periodicTimeBegin;
    float stateTimeBegin;

    //AudioSource source;
    GameObject target = null;
    Rigidbody rb;
    Vector3 targetDir;
    

    // Constantes
    const float RUNNING_DURATION = 1f;
    const float ANGLE_TOL = 1f;
    const float SPAWN_PERIOD = 10f;

    enum States
    {
        CROWLING,
        TURNING,
        RUNNING,
        LOOKUP,
        VOMITING,
        ATTACKING
    }

    States state = States.TURNING;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //source = GetComponent<AudioSource>();

        periodicTimeBegin = Time.time;

        CreateSegments();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckStatesTransitions();

        switch (state)
        {
            case States.CROWLING:
                target = GameObject.FindGameObjectsWithTag("Player")[0];
                break;
            case States.TURNING:
                StateTurning();
                break;
            case States.RUNNING:
                StateRunning();
                break;
            case States.LOOKUP:
                StateLookUp();
                break;
            case States.VOMITING:
                StateVomiting();
                break;
            default:
                break;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //source.Play();
    }

    void CheckStatesTransitions()
    {
        if (target == null)
        {
            ChangeState(States.CROWLING);
            return;
        }

        if (IsPeriodicTime())
        {
            ChangeState(States.LOOKUP);
            return;
        }

        switch (state)
        {
            case States.CROWLING:
                if (target)
                    ChangeState(States.TURNING);
                break;
            case States.TURNING:
                if (IsDirectedToPlayer())
                    ChangeState(States.RUNNING);
                break;
            case States.RUNNING:
                if (GetStateDuration() >= RUNNING_DURATION)
                    ChangeState(States.TURNING);
                break;
            case States.LOOKUP:
                if (IsLookingUp())
                    ChangeState(States.VOMITING);
                break;
            default:
                break;

        }
    }

    void StateTurning()
    {
        // Determine which direction to rotate towards
        targetDir = target.transform.position - transform.position;
        targetDir.Normalize();

        // The step size is equal to speed times frame time.
        float singleStep = rotSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void StateRunning()
    {
        rb.AddForce(targetDir * force * Time.deltaTime, ForceMode.Impulse);
    }

    void StateLookUp()
    {
        // Determine which direction to rotate towards
        targetDir = Vector3.up;

        // The step size is equal to speed times frame time.
        float singleStep = rotSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void StateVomiting()
    {
        Instantiate(spawn, transform.position, transform.rotation);
        ChangeState(States.TURNING);
    }

    void CreateSegments()
    {
        GameObject previous = this.gameObject;

        // Instantiate all segments
        for (int i = 0; i < nSeg; i++)
        {
            GameObject segObj = Instantiate(prefab, previous.transform.position - previous.transform.forward * nextSegDistance, previous.transform.rotation);
            Enemy_PsychoCentipedeSegment seg = segObj.GetComponent<Enemy_PsychoCentipedeSegment>();
            seg.segNum = i;
            seg.previous = previous;

            previous = segObj;
        }
    }

    bool IsDirectedToPlayer()
    {
        float angle = Vector3.Angle(targetDir, transform.forward);

        return angle <= ANGLE_TOL;
    }

    bool IsLookingUp()
    {
        float angle = Vector3.Angle(Vector3.up, transform.forward);

        return angle <= ANGLE_TOL;
    }

    void ChangeState(States newState)
    {
        stateTimeBegin = Time.time;
        state = newState;
    }

    float GetStateDuration()
    {
        return Time.time - stateTimeBegin;
    }

    bool IsPeriodicTime()
    {
        float now = Time.time;
        if (now - periodicTimeBegin >= SPAWN_PERIOD)
        {
            periodicTimeBegin = now;
            return true;
        }
        return false;
    }

}
