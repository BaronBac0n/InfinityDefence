using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemyTargets;

    public Transform[] enemySpawns;

    public GameObject[] enemyPrefabs;

    public GameObject[] portalPrefabs;

    public PhotonView pV;

    public float spawnRate;

    void Start()
    {
        pV = GetComponent<PhotonView>();
        SpawnEnemy();
    }
    
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnRate);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        pV.RPC("RPC_SpawnEnemy", RpcTarget.All);
    }

    [PunRPC]
    void RPC_SpawnEnemy()
    {
        if (!pV.IsMine)
            return;

        //choose a random spawner
        Transform chosenSpawner = enemySpawns[Random.Range(0, enemySpawns.Length)];

        //choose a random enemy to spawn
        GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        //choose a random portal
        GameObject chosenPortal = portalPrefabs[Random.Range(0, portalPrefabs.Length)];

        //chose a random target to go toward
        Transform chosenTarget = enemyTargets[Random.Range(0, enemyTargets.Length)];

        //spawn the portal
        GameObject portalClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Purple Portal"), new Vector3(chosenSpawner.position.x, chosenSpawner.position.y + 1, chosenSpawner.position.z - 5), Quaternion.identity);
        portalClone.transform.LookAt(chosenTarget);

        //spawn it
        GameObject enemyClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BasicEnemy"), chosenSpawner.position, Quaternion.identity);

        //make it look at the target
        enemyClone.GetComponent<Enemy>().target = chosenTarget;
        StartCoroutine(Wait());
    }
}
