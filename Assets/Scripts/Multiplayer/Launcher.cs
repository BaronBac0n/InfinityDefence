using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Singleton
    public static Launcher instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Launcher found");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField]
    InputField roomNameInputField;

    [SerializeField]
    Text errorText;

    [SerializeField]
    Text roomNameText;

    [SerializeField]
    Transform roomListContent;

    [SerializeField]
    GameObject roomListItemPrefab;

    [SerializeField]
    Transform playerListContent;

    [SerializeField]
    GameObject playerListItemPrefab;
    
    public string gameSceneName;

    [SerializeField]
    Button startGameButton;

    private void Start()
    {
        print("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Master");
        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenu("control");
        print("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
            return;

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.instance.OpenMenu("room");

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.interactable = PhotonNetwork.IsMasterClient;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.instance.OpenMenu("control");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(gameSceneName);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.interactable = PhotonNetwork.IsMasterClient;
    }
}