using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocket : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
