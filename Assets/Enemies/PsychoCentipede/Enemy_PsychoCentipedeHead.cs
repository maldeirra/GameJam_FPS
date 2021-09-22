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
    
    
    //AudioSource source;
    GameObject target = null;
    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();

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




        //source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        //transform.LookAt(target.transform);

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.Normalize();

        // The step size is equal to speed times frame time.
        float singleStep = rotSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        //rb.AddForce(transform.forward * force * Time.deltaTime);
        //rb.AddForce(targetDirection * force * Time.deltaTime);
        //rb.AddForce(transform.forward * force * Time.deltaTime);
        transform.position += targetDirection * force * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        //source.Play();

    }

}
