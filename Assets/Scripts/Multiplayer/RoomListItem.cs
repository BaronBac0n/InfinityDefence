using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public RoomInfo info;

    public void SetUp(RoomInfo roomInfo)
    {
        info = roomInfo;
        text.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.instance.JoinRoom(info);
    }
}
