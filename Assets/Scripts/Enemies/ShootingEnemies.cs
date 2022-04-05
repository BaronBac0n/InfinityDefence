using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShootingEnemies : Enemy
{
    public GameObject shooterSpawner1;
    public GameObject shooterSpawner2;

    public float startShootingDistance;
    public float stopMovingDistance;

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
        if (flipLookAt)
            transform.LookAt(target.position);
        else
            transform.LookAt(2 * transform.position - target.position);

        if (Vector3.Distance(transform.position, target.transform.position) <= startShootingDistance)
        {
            if (canShoot)
                StartCoroutine(Shoot());
        }
        else if(Vector3.Distance(transform.position, target.transform.position) >= stopMovingDistance)
        {
            MoveTowardTarget();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bulletClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemy Bullet"), shooterSpawner1.transform.position, Quaternion.identity);
        GameObject bulletClone1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemy Bullet"), shooterSpawner2.transform.position, Quaternion.identity);
        bulletClone.SetActive(true);
        bulletClone1.SetActive(true);
        bulletClone.transform.LookAt(target.transform);
        bulletClone1.transform.LookAt(target.transform);
        bulletClone.GetComponent<BulletManager>().target = target;
        bulletClone1.GetComponent<BulletManager>().target = target;

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    public void TakeDamage(float amount)
    {
        print("working");
        pV.RPC("RPC_TakeDamage", RpcTarget.All, amount);

        //currentHealth -= amount;
        //if(DeathCheck())
        //{
        //    Die();
        //}
    }

    [PunRPC]
    void RPC_TakeDamage(float amount)
    {
        if (!pV.IsMine)
            return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
