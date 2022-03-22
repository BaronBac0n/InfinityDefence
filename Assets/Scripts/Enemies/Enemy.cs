using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Photon.Pun;
using System.IO;

public class Enemy : MonoBehaviour
{
    public int maxHealth, currentHealth;

    public Vector2 speed;
    public int damageToPlayer;
    public int partsDropped;

    [HideInInspector]
    public Transform target;

    public GameObject deathEffect;

    public PhotonView pV;

    float randSpeed;

    PlayerManager playerManager;

    void Start()
    {
        pV = GetComponent<PhotonView>();
        randSpeed = Random.Range(speed.x, speed.y);
        currentHealth = maxHealth;
    }

    void Update()
    {
        MoveTowardTarget();

        //look at target
        transform.LookAt(2 * transform.position - target.position);
    }

    public void MoveTowardTarget()
    {
        float step = randSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void TakeDamage(int amount)
    {
        pV.RPC("RPC_TakeDamage", RpcTarget.All, amount);

        //currentHealth -= amount;
        //if(DeathCheck())
        //{
        //    Die();
        //}
    }

    [PunRPC]
    void RPC_TakeDamage(int amount)
    {
        print("A");
        if (!pV.IsMine)
            return;
        print("B");
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //PartsTracker.instance.AddParts(partsDropped);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Explosion_01"), transform.position, transform.rotation);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyTarget")
        {
            PlayerHealth.instance.TakeDamage(damageToPlayer);
            GameObject deathEffectClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Explosion_01"), transform.position, transform.rotation);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
