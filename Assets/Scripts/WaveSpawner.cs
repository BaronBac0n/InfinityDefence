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

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject startWaveButton;
    public GameObject waveSpawner;
    public GameObject winMessage;
    public GameObject speedUpButton;

    private int positionInWave = 0;
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    public Text currentWaveDisplay;

    private bool canSpawn = true;
    public bool isSpawning = false;

    private void Update()
    {
        if (isSpawning == true)
        {
            currentWave = waves[currentWaveNumber];
            currentWaveDisplay.text = "Wave: " + currentWave.waveName;
            SpawnWave();
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Zombie");

            //end of the wave
            if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
            {
                positionInWave = 0;
                startWaveButton.SetActive(true);
                currentWaveNumber++;
                isSpawning = false;
                canSpawn = true;
            }
            if (totalEnemies.Length == 0 && currentWaveNumber == 25)
            {
                winMessage.SetActive(true);
                Destroy(startWaveButton);
                Destroy(speedUpButton);
                Time.timeScale = 1;
            }
        }
    }

    void SpawnWave()
    {
        startWaveButton.SetActive(false);

        if (canSpawn && nextSpawnTime < Time.time)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(currentWave.enemies[positionInWave], randomPoint.position, Quaternion.identity);
            positionInWave++;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (positionInWave >= currentWave.enemies.Length)
            {
                canSpawn = false;
            }
        }
    }

    public void setSpawning()
    {
        isSpawning = !isSpawning;
    }
}
