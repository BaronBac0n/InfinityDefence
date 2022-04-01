using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakCannon : Gun
{
    public float pelletSpeed = 150;
    public int pelletCount = 5;
    public float spreadFactor = 0.01f;

    public override void Shoot()
    {
        GameObject pellet;
        for (var i = 0; i < pelletCount; i++)
        {
            Quaternion pelletRot = transform.rotation;
            pelletRot.x += Random.Range(-spreadFactor, spreadFactor);
            pelletRot.y += Random.Range(-spreadFactor, spreadFactor);
            pellet = Instantiate(projectile, projectileSpawner.transform.position, pelletRot);
            pellet.GetComponent<Rigidbody>().velocity = transform.forward * pelletSpeed;
        }
    }

    public override void StopShoot()
    {
    }
}


