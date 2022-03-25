using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : Gun
{
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
        bullet.transform.LookAt(targetPoint);
        bullet.GetComponent<Rigidbody>().velocity = (targetPoint - projectileSpawner.transform.position).normalized * bulletSpeed;

        //set the damage of the bullet
        bullet.GetComponent<BulletScript>().damage = damage;
        
    }
}
