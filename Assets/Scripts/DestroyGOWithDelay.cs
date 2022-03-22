using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGOWithDelay : MonoBehaviour
{
    public float timer;

    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(timer);

        PhotonNetwork.Destroy(this.gameObject);
    }
}
