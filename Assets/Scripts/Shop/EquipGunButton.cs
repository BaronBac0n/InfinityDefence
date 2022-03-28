using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGunButton : MonoBehaviour
{
    public int buttonNumber;
    public Gun LgunInButton;
    public Gun RgunInButton;

    Image gunSprite;

    Text gunDescription;

    Button equipLeft, equipRight;

    private void Start()
    {
        LgunInButton = ShopManager.instance.playerLeftGuns[buttonNumber];
        RgunInButton = ShopManager.instance.playerRightGuns[buttonNumber];

        gunSprite = transform.GetChild(0).GetComponent<Image>();
        gunDescription = transform.GetChild(1).GetComponent<Text>();
        equipLeft = transform.GetChild(2).GetComponent<Button>();
        equipRight = transform.GetChild(3).GetComponent<Button>();

        gunSprite.sprite = LgunInButton.sprite;
        gunDescription.text = RgunInButton.description;

        if (!LgunInButton.purchased)
        {
            equipLeft.GetComponentInChildren<Text>().text = "Equip Left - " + LgunInButton.cost.ToString();
            equipRight.GetComponentInChildren<Text>().text = "Equip Right - " + LgunInButton.cost.ToString();
        }
        else
        {
            equipLeft.GetComponentInChildren<Text>().text = "Equipped";
            equipLeft.interactable = false;

            equipRight.GetComponentInChildren<Text>().text = "Equipped";
            equipRight.interactable = false;
        }
    }

    public void OnEquipLeft()
    {
        //check if we have enough to purchase
        if (PartsTracker.instance.parts >= LgunInButton.cost)
        {
            //set all the equip buttons interactable and fix texts
            EquipGunButton[] buttons = GameObject.FindObjectsOfType<EquipGunButton>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].equipLeft.interactable = true;

                if (buttons[i].LgunInButton.purchased)
                    buttons[i].equipLeft.GetComponentInChildren<Text>().text = "Equip Left";
                else
                    buttons[i].equipLeft.GetComponentInChildren<Text>().text = "Equip Left - " + buttons[i].LgunInButton.cost.ToString();
            }

            if (!LgunInButton.purchased)
                PartsTracker.instance.RemoveParts(LgunInButton.cost);

            LgunInButton.purchased = true;
            equipLeft.GetComponentInChildren<Text>().text = "Equipped";

            equipLeft.interactable = false;

            Gun[] leftGuns = ShopManager.instance.playerLeftGuns;

            //set all guns disabled
            for (int i = 0; i < leftGuns.Length; i++)
            {
                leftGuns[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < leftGuns.Length; i++)
            {
                if (leftGuns[i].gunName == LgunInButton.gunName)
                    leftGuns[i].gameObject.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(PartsTracker.instance.FlashText());
        }
    }

    public void OnEquipRight()
    {
        if (PartsTracker.instance.parts >= RgunInButton.cost)
        {
            //set all the equip buttons interactable and fix texts
            EquipGunButton[] buttons = GameObject.FindObjectsOfType<EquipGunButton>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].equipRight.interactable = true;

                if (buttons[i].RgunInButton.purchased)
                    buttons[i].equipRight.GetComponentInChildren<Text>().text = "Equip Right";
                else
                    buttons[i].equipRight.GetComponentInChildren<Text>().text = "Equip Right - " + buttons[i].RgunInButton.cost.ToString();
            }

            if (!RgunInButton.purchased)
                PartsTracker.instance.RemoveParts(RgunInButton.cost);

            RgunInButton.purchased = true;
            equipRight.GetComponentInChildren<Text>().text = "Equipped";
            equipRight.interactable = false;

            Gun[] rightGuns = ShopManager.instance.playerRightGuns;

            //set all guns disabled
            for (int i = 0; i < rightGuns.Length; i++)
            {
                rightGuns[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < rightGuns.Length; i++)
            {
                if (rightGuns[i].gunName == RgunInButton.gunName)
                    rightGuns[i].gameObject.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(PartsTracker.instance.FlashText());
        }
    }
}
