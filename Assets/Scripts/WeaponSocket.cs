using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocket : MonoBehaviour
{
    private GameObject handled = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachObject(GameObject newObj)
    {
        //if (!newObj)
        //    return;

        handled = newObj;

        Transform theParent = transform.Find("PlayerCameraRoot/WeaponSocket/Handle");

        handled.transform.SetParent(theParent.transform, false);

        Transform theHandle = handled.transform.Find("Functional/Handle");

        handled.transform.localPosition = -theHandle.localPosition;
        //handled.transform.position = theHandle.position;


        //handled.transform.position = transform.Find("Handle").position - handled.transform.Find("Handle").position;


    }
}
