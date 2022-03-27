using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGunButton : MonoBehaviour
{

    public Gun gunInButton;
    public GameObject leftGunHolder;
    public Gun[] playerLeftGuns;

    Image gunSprite;

    Text gunDescription;

    Button equipLeft, equipRight;

    private void Start()
    {
        gunSprite = transform.GetChild(0).GetComponent<Image>();
        gunDescription = transform.GetChild(1).GetComponent<Text>();
        equipLeft = transform.GetChild(2).GetComponent<Button>();
        equipRight = transform.GetChild(3).GetComponent<Button>();

        gunSprite.sprite = gunInButton.sprite;
        gunDescription.text = gunInButton.description;

        if (!gunInButton.purchased)
        {
            equipLeft.GetComponentInChildren<Text>().text = "Equip Left - " + gunInButton.cost.ToString();
            equipRight.GetComponentInChildren<Text>().text = "Equip Right - " + gunInButton.cost.ToString();
        }
        else
        {
            equipLeft.GetComponentInChildren<Text>().text = "Equip Left";
            equipRight.GetComponentInChildren<Text>().text = "Equip Right";
        }


    }

    public void OnEquipLeft()
    {
        leftGunHolder = GameObject.FindGameObjectWithTag("LeftGun");

        //set up the player gun array
        int children = leftGunHolder.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            playerLeftGuns[i] = leftGunHolder.transform.GetChild(i).gameObject.GetComponent<Gun>();
        }

        //set all guns disabled
        for (int i = 0; i < playerLeftGuns.Length; i++)
        {
            playerLeftGuns[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < playerLeftGuns.Length; i++)
        {
            if (playerLeftGuns[i].gunName == gunInButton.gunName)
                playerLeftGuns[i].gameObject.SetActive(true);
        }
    }
}
