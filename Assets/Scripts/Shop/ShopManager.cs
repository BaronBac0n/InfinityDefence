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

    [HideInInspector]
    public GameObject playerLeftGunHolder;    
    public Gun[] playerLeftGuns;
    
    public GameObject playerRightGunHolder;    
    public Gun[] playerRightGuns;

    void Start()
    {
        FindLeftGuns();
        FindRightGuns();
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

    public void FindLeftGuns()
    {
        playerLeftGunHolder = GameObject.FindGameObjectWithTag("LeftGun");

        //set up the player gun array
        int children = playerLeftGunHolder.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            playerLeftGuns[i] = playerLeftGunHolder.transform.GetChild(i).gameObject.GetComponent<Gun>();
        }
    }

    public void FindRightGuns()
    {
        playerRightGunHolder = GameObject.FindGameObjectWithTag("RightGun");

        //set up the player gun array
        int children = playerRightGunHolder.transform.childCount;
        print(children);
        for (int i = 0; i < children; i++)
        {
            playerRightGuns[i] = playerRightGunHolder.transform.GetChild(i).gameObject.GetComponent<Gun>();
        }
    }

}
