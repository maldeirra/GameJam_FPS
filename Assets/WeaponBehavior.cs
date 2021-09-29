using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float autoDelay = 0.1f;
    public GameObject muzzleFlash;

    private AudioSource audioSource;
    bool shootLocked = false;
    bool shootSignal = false;
    Transform visualPart;
    Transform muzzlePart;
    Vector3 originPos;
    Vector3 reculPos;
    float reculDist = 0.15f;
    float retSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        visualPart = transform.Find("Visual");
        muzzlePart = transform.Find("Functional/Muzzle");

        originPos = visualPart.localPosition;
        reculPos = visualPart.localPosition - new Vector3(0, 0, reculDist);
    }

    // Update is called once per frame
    void Update()
    {
        ShootLogic();

        visualPart.transform.localPosition = Vector3.MoveTowards(visualPart.transform.localPosition, originPos, retSpeed * Time.deltaTime);
    }

    public void Shoot(bool pressed)
    {
        shootSignal = pressed;
    }

    void ShootLogic()
    {
        if (!shootSignal)
        {
            StopShoot();
            return;
        }

        if (shootLocked)
            return;

        ActionShoot();
    }

    void ActionShoot()
    {
        shootLocked = true;

        // Son
        audioSource.Play();
        
        // Recul
        visualPart.transform.localPosition = reculPos;

        // Petite explosion au niveau du canon
        AddCanonExplosion();

        Invoke("ReleaseNewBullet", autoDelay);
    }

    void StopShoot()
    {
        audioSource.Stop();
    }

    void ReleaseNewBullet()
    {
        shootLocked = false;
    }

    void AddCanonExplosion()
    {
        GameObject flash = Instantiate(muzzleFlash, muzzlePart.position, muzzlePart.rotation);
        Destroy(flash, 0.04f);
    }
}
