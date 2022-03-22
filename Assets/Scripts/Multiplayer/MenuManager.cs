using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    #region Singleton
    public static MenuManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MenuManager found");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField]
    Menu[] menus;

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
