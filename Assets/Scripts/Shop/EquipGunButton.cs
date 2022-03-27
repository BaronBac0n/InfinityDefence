using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGunButton : MonoBehaviour
{

    public Gun gunInButton;

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
        Gun[] leftGuns = ShopManager.instance.playerLeftGuns;

        //set all guns disabled
        for (int i = 0; i < leftGuns.Length; i++)
        {
            leftGuns[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < leftGuns.Length; i++)
        {
            if (leftGuns[i].gunName == gunInButton.gunName)
                leftGuns[i].gameObject.SetActive(true);
        }
    }

    public void OnEquipRight()
    {

        Gun[] rightGuns = ShopManager.instance.playerRightGuns;

        //set all guns disabled
        for (int i = 0; i < rightGuns.Length; i++)
        {
            rightGuns[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < rightGuns.Length; i++)
        {
            if (rightGuns[i].gunName == gunInButton.gunName)
                rightGuns[i].gameObject.SetActive(true);
        }
    }
}