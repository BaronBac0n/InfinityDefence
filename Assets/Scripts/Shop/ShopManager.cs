using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Singleton
    public static ShopManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ShopManager found");
            return;
        }
        instance = this;
    }
    #endregion
    public GameObject shopPanel;

    public GameObject showShopButton;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        showShopButton.SetActive(false);
        PlayerRotate.instance.enabled = false;
    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
        showShopButton.SetActive(true);
        PlayerRotate.instance.enabled = true;
    }

}
