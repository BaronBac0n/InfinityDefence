using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public GameObject[] enemies;
    public float spawnInterval;
    public int reward;
}
public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;

    public Transform[] enemyTargets;
    public Transform[] enemySpawns;

    public GameObject[] enemyPrefabs;
    public GameObject[] portalPrefabs;

    public Button startWaveButton;

    public PhotonView pV;

    public GameObject winMessage;

    public float spawnRate;
    public float nextSpawnTime;

    public bool canSpawn = true;
    public bool isSpawning;

    private int positionInWave = 0;
    public int currentWaveNumber;
    void Start()
    {
        pV = GetComponent<PhotonView>();
    }
    
    void Update()
    {
        if(startWaveButton == null)
        {
            startWaveButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
            startWaveButton.onClick.AddListener(this.setSpawning);

            startWaveButton.interactable = PhotonNetwork.IsMasterClient;
        }
        print(currentWaveNumber);
        if (isSpawning == true)
        {
            //currentWaveDisplay.text = "Wave: " + currentWave.waveName;
            SpawnWave();
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            //end of the wave
            if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber != waves.Length)
            {
                positionInWave = 0;
                startWaveButton.gameObject.SetActive(true);
                currentWaveNumber++;
                isSpawning = false;
                canSpawn = true;
            }
            if (totalEnemies.Length == 0 && currentWaveNumber == waves.Length)
            {
                winMessage = GameObject.FindGameObjectWithTag("You Win Panel").transform.GetChild(0).gameObject;
                winMessage.SetActive(true);
                Destroy(startWaveButton);
                //Destroy(speedUpButton);
                Time.timeScale = 1;
            }
        }
    }

    public void SpawnWave()
    {
        startWaveButton.gameObject.SetActive(false);

        if (canSpawn && nextSpawnTime < Time.time)
        {
            SpawnEnemy();
            positionInWave++;
            nextSpawnTime = Time.time + waves[currentWaveNumber].spawnInterval;

            if (positionInWave >= waves[currentWaveNumber].enemies.Length)
            {
                canSpawn = false;
            }
        }
    }
    public void setSpawning()
    {
        isSpawning = !isSpawning;
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
        //GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        //choose a random portal
        GameObject chosenPortal = portalPrefabs[Random.Range(0, portalPrefabs.Length)];

        //chose a random target to go toward
        Transform chosenTarget = enemyTargets[Random.Range(0, enemyTargets.Length)];

        //spawn the portal
        GameObject portalClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Purple Portal"), new Vector3(chosenSpawner.position.x, chosenSpawner.position.y + 1, chosenSpawner.position.z - 5), Quaternion.identity);
        portalClone.transform.LookAt(chosenTarget);

        //spawn it
        GameObject enemyClone = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", waves[currentWaveNumber].enemies[positionInWave].name), chosenSpawner.position, Quaternion.identity);

        //make it look at the target
        enemyClone.GetComponent<Enemy>().target = chosenTarget;
        //StartCoroutine(Wait());
    }
}
