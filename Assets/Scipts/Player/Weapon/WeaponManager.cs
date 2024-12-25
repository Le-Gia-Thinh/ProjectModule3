using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate ;
    [SerializeField] bool semiAuto ;
    float fireRateTimer ;
    [Header("Bullet Properties")] 
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barelPos;
    [SerializeField] int bulletsPerShot ;
    [SerializeField] float bulletVelocity ;
    AimStateManager aim ;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager> ();
        fireRateTimer = fireRate;
    }
    void Update ()
    {
        if (ShouldFire()) Fire();
    }
    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if ( fireRateTimer < fireRate)return false;
        if ( semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }
    void Fire()
    {
        fireRateTimer = 0f;
        barelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        for( int i = 0; i < bulletsPerShot ; i ++ )
        {
            GameObject currentBullet = Instantiate( bullet , barelPos.position, barelPos.rotation ) ;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barelPos.forward * bulletVelocity , ForceMode.Impulse);
        }
    }
}
