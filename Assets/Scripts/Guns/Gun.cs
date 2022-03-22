using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    public bool isLeftGun;
    public int damage = 10;
    public float range;
    public float fireRate = 15;
    public float reloadTime = 15;

    public float bulletSpeed;

    public int maxAmmoInMag;
    public int ammoInMag;

    public Camera fpsCam;

    public bool reloading = false;

    public GameObject projectileSpawner;
    public GameObject projectile;

    public ParticleSystem muzzleFlash;

    public  float nextTimeToFire = 0;

    [HideInInspector]
    public Animator anim;

    public bool canShoot = true;

    public Text ammoText;

    public abstract void Shoot();

    public PhotonView pV;
}
