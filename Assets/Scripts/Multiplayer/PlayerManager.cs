using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView pV;
    public GameObject playerUI;
    private void Awake()
    {
        pV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(pV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        print("A");
        Transform spawnpoint;

        if (PhotonNetwork.IsMasterClient)
            spawnpoint = GameObject.Find("HostSpawn").transform;
        else
            spawnpoint = GameObject.Find("Player2Spawn").transform;

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, Quaternion.identity);
        Instantiate(playerUI);
       // playerClone.GetComponent<PlayerRotate>().enabled = true;
    }
}
