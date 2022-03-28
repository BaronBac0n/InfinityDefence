using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShootingEnemies : Enemy
{
    public GameObject shooterSpawner;

    public float timeBetweenShots;

    private bool canShoot = true;
    void Start()
    {
        pV = GetComponent<PhotonView>();
        randSpeed = Random.Range(speed.x, speed.y);
        currentHealth = maxHealth;
    }

    void Update()
    {
        transform.LookAt(2 * transform.position - target.position);

        if (Vector3.Distance(transform.position, target.transform.position) <= 25.5f)
        {
            if(canShoot)
            StartCoroutine(Shoot());
        }
        else
        {
            MoveTowardTarget();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bulletClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemy Bullet"), shooterSpawner.transform.position, Quaternion.identity);
        bulletClone.SetActive(true);
        bulletClone.transform.LookAt(target.transform);
        bulletClone.GetComponent<BulletManager>().target = target;

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}
