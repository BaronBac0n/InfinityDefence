using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Laser : Gun
{
    public GameObject bulletClone = null;
    bool isShooting = false;
    public bool isOverheating = false;

    private void Update()
    {
        base.Update();
        if (!isShooting)
        {
            if (ammoInMag < maxAmmoInMag)
                ammoInMag++;
        }
        else
        {
            if (ammoInMag > 0 || !isOverheating)
                ammoInMag--;
        }
        if(ammoInMag <=0)
        {
            StopShoot();
            isOverheating = true;
        }

        if (isOverheating)
        {
            canShoot = false;
            ammoText.color = Color.red;
            ammoText.text = "OVERHEAT";
        }
        else
        {
            canShoot = true;
            ammoText.color = Color.white;
        }

        if (ammoInMag == maxAmmoInMag)
            isOverheating = false;
    }

    public override void Shoot()
    {
        if (bulletClone == null)
        {

            isShooting = true;
            ammoInMag -= 1;
            if (ammoInMag >= 1 && !isOverheating)
            {
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
                bulletClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "LaserStaticBlue"), projectileSpawner.transform.position, projectileSpawner.transform.rotation);
                bulletClone.transform.LookAt(targetPoint);
                bulletClone.transform.SetParent(projectileSpawner.transform);
            }
            else StopShoot();
        }
    }

    public override void StopShoot()
    {
        isShooting = false;
        PhotonNetwork.Destroy(bulletClone);
    }
}
