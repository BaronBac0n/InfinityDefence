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
        Transform spawnpoint;

        if (PhotonNetwork.IsMasterClient)
            spawnpoint = GameObject.Find("HostSpawn").transform;
        else
            spawnpoint = GameObject.Find("Player2Spawn").transform;

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, Quaternion.identity);
        GameObject UIClone = Instantiate(playerUI);
        GameObject hText = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Health Text"), Vector3.zero, Quaternion.identity);
        hText.transform.parent = UIClone.transform.GetChild(0).transform.GetChild(2).transform;
        hText.GetComponent<Transform>().position = hText.transform.parent.position;

        GameObject pText = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Parts text"), Vector3.zero, Quaternion.identity);
        pText.transform.parent = UIClone.transform.GetChild(5).transform;
        pText.GetComponent<Transform>().position = pText.transform.parent.position;
    }
}
