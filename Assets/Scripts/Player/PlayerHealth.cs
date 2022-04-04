using EZCameraShake;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    #region Singleton
    public static PlayerHealth instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerHealth found");
            return;
        }
        instance = this;
    }
    #endregion

    public int maxHealth;
    public int currentHealth;

    public Text healthText;

    public GameObject[] camerasToShake;

    void Start()
    {
        currentHealth = maxHealth;
        healthText = GameObject.FindGameObjectWithTag("Health Text").GetComponent<Text>();
        healthText.text = currentHealth.ToString();
    }
    
    void Update()
    {
        
    }
    [PunRPC]
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthText.text = currentHealth.ToString();
        if (DeathCheck())
            Die();
    }

    public bool DeathCheck()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }

    public void Die()
    {
        print("YOU DEAD");
    }
}
