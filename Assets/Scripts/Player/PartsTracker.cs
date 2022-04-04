using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsTracker : MonoBehaviour
{
    #region Singleton
    public static PartsTracker instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PartsTracker found");
            return;
        }
        instance = this;
    }
    #endregion

    public int parts;

    Text partsText;
    public PhotonView pV;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        partsText = GetComponent<Text>();
        UpdateText();
    }

    public void AddParts(int amount)
    {
        pV.RPC("RPC_AddParts", RpcTarget.All, amount);
    }

    [PunRPC]
    public void RPC_AddParts(int amount)
    {
        //if (pV.IsMine)
        parts += amount;

        partsText.text = parts.ToString();
        //UpdateText();
    }

    public void RemoveParts(int amount)
    {
        parts -= amount;
        partsText.text = parts.ToString();
        //UpdateText();
    }

    public void UpdateText()
    {
        partsText.text = parts.ToString();
    }

    public IEnumerator FlashText()
    {
        Color initColor = partsText.color;

        partsText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        partsText.color = initColor;
        yield return new WaitForSeconds(0.1f);
        partsText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        partsText.color = initColor;
    }
}
