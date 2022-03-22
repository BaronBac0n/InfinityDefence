using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{

    public Gun leftGun;
    public Gun rightGun;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            leftGun.Shoot();
        }
    }
}
