using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject impactEffect;

    [HideInInspector]
    public int damage;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BulletImpactBlue"), transform.position, transform.rotation);

        if(other.tag == "Enemy")
        {
            other.transform.root.GetComponent<Enemy>().TakeDamage(damage);
        }


        PhotonNetwork.Destroy(this.gameObject);
    }

   
}
