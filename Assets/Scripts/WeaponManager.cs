using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public GameObject currentWeapon = null;


    // Start is called before the first frame update
    void Start()
    {
        AttachWeaponIfNeeded();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(InputValue value)
    {
        bool shoot = value.isPressed;

        WeaponBehavior weapon = currentWeapon.GetComponent<WeaponBehavior>();

        weapon.Shoot(shoot);
    }

    void AttachWeaponIfNeeded()
    {
        //if (!currentWeapon)
        //    return;

        WeaponSocket socket = gameObject.GetComponent<WeaponSocket>();

        if (!socket)
        {
            Debug.Log("merde");
        }

        socket.AttachObject(currentWeapon);

    }
}
