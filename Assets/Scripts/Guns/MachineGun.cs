using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : Gun
{
    private void Start()
    {
        if (isLeftGun)
            ammoText = GameObject.FindGameObjectWithTag("Left ammo count").GetComponent<Text>();
        else
            ammoText = GameObject.FindGameObjectWithTag("Right ammo count").GetComponent<Text>();

        range = Mathf.Infinity;
        fpsCam = Camera.main;
        anim = GetComponent<Animator>();
        pV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!pV.IsMine)
            return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        if (reloading)
        {
            ammoText.color = Color.red;
            ammoText.text = "RELOADING";
        }
        else
        {
            ammoText.color = Color.white;
            ammoText.text = ammoInMag + "/ ∞";
        }

        //THERE IS A BUG WHEN SPAMMING RELOAD
        if (Input.GetAxis("Reload") > 0 && anim.GetBool("isReloading") == false)
        {
            StartCoroutine(Reload());
        }

        if (canShoot)
        {
            if (isLeftGun)
            {
                if (Input.GetAxis("Fire1") > 0.1f && Time.time >= nextTimeToFire)
                {
                    if (ammoInMag > 0)
                    {
                        //shoot
                        nextTimeToFire = Time.time + 1 / fireRate;
                        Shoot();
                        //anim.SetBool("isShooting", true);
                    }
                    else
                    {
                        StartCoroutine(Reload());
                    }
                }
                else
                {
                    //anim.SetBool("isShooting", false);
                }
            }
            else
            {
                if (Input.GetAxis("Fire2") > 0.1f && Time.time >= nextTimeToFire)
                {
                    if (ammoInMag > 0)
                    {
                        //shoot
                        nextTimeToFire = Time.time + 1 / fireRate;
                        Shoot();
                        //anim.SetBool("isShooting", true);
                    }
                    else
                    {
                        StartCoroutine(Reload());
                    }
                }
                else
                {
                    //anim.SetBool("isShooting", false);
                }
            }

        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        //anim.SetBool("isShooting", false);
    }

    IEnumerator Reload()
    {
        anim.SetBool("isReloading", true);

        reloading = true;

        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("isReloading", false);
        ammoInMag = maxAmmoInMag;
        reloading = false;
    }

    public override void Shoot()
    {
        ammoInMag -= 1;

        muzzleFlash.Play();
        muzzleFlash.GetComponent<AudioSource>().Play();
        
        // Create a ray from the camera going through the middle of your screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000); // You may need to change this value according to your needs
                                              // Create the bullet and give it a velocity according to the target point computed before
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet2Blue"), projectileSpawner.transform.position, projectileSpawner.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (targetPoint - projectileSpawner.transform.position).normalized * bulletSpeed;

        //set the damage of the bullet
        bullet.GetComponent<BulletScript>().damage = damage;

        StartCoroutine(Wait(.1f));
    }
}
