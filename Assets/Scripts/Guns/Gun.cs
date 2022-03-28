using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public bool isLeftGun;
    public int damage = 10;
    public float range;
    public float fireRate = 15;
    public float reloadTime = 15;
    public bool isProjectileWeapon;

    public int cost;

    public bool purchased;

    public float bulletSpeed;

    public int maxAmmoInMag;
    public int ammoInMag;

    public Camera fpsCam;

    public bool reloading = false;

    public GameObject projectileSpawner;
    public GameObject projectile;

    public ParticleSystem muzzleFlash;

    public float nextTimeToFire = 0;

    [HideInInspector]
    public Animator anim;

    public bool canShoot = true;

    public Text ammoText;

    public Sprite sprite;

    [TextArea]
    public string description;

    public abstract void Shoot();

    public PhotonView pV;


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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && ShopManager.instance.shopPanel.activeInHierarchy == false)
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
                    if (isProjectileWeapon)
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
                        if (ammoInMag > 0)
                        {
                            //shoot
                            Shoot();
                        }

                    }
                }
            }
            else
            {
                if (Input.GetAxis("Fire2") > 0.1f && Time.time >= nextTimeToFire)
                {
                    if (isProjectileWeapon)
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
                        if (ammoInMag > 0)
                        {
                            Shoot();
                        }
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
    }
}
