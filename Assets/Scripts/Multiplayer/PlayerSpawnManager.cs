using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    #region Singleton
    public static PlayerSpawnManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerSpawnManager found");
            return;
        }
        instance = this;
    }
    #endregion

    GameObject[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<GameObject>();
    }
    
}
