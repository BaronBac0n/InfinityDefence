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

    public Text partsText;

    private void Start()
    {
        UpdateText();
    }

    public void AddParts(int amount)
    {
        parts += amount;
        UpdateText();
    }

    public void RemoveParts(int amount)
    {
        parts -= amount;
        UpdateText();
    }

    public void UpdateText()
    {
        partsText.text = parts.ToString();
    }
}
