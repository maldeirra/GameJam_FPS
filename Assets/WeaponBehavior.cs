using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float autoDelay = 0.1f;
    public float muzzleFlashTime = 0.2f;
    public float bulletForce = 500f;

    public GameObject bullet;
    public GameObject muzzleFlash;

    AudioSource audioSource;
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

        // Balle + petite explosion au niveau du canon
        AddBullet();
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
        
        flash.transform.SetParent(muzzlePart);

        Destroy(flash, muzzleFlashTime);
    }

    void AddBullet()
    {
        GameObject newBullet = Instantiate(bullet, muzzlePart.position, muzzlePart.rotation);

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        rb.AddForce(newBullet.transform.forward * bulletForce);
    }
}
