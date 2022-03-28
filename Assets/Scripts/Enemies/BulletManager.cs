using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damage;
    public Transform target;
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyTarget")
        {
            PlayerHealth.instance.TakeDamage(damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
