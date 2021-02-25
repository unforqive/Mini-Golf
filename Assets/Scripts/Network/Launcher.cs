using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Variables
    public static Launcher Instance;

    public Animator mainCameraAnimation;
    public MenuController menuController;

    private int timer;
    private bool startTimer;

    public GameObject loadingText;
    public GameObject unloadingText;

    private int timer2;
    private bool startTimer2;

    private int timer3;
    private bool startTimer3;

    private int timer4;
    private bool startTimer4;

    private bool alreadyConnected;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    //[SerializeField] GameObject startGameButton;

    public EnterName enterName;
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        alreadyConnected = false;
        timer = 0;
        timer2 = 0;
        timer3 = 0;
        timer4 = 0;
    }

    public void JoinMultiplayer()
    {
        playerListItemPrefab = null;
        playerListContent = null;

        PhotonNetwork.ConnectUsingSettings();

        //call timer
        mainCameraAnimation.SetBool("CameraUp", true);
        mainCameraAnimation.SetBool("CameraDown", false);
        startTimer = true;
        loadingText.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        startTimer2 = true;

        Debug.Log("Joined Lobby");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Gamemode");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room Menu");
        Debug.Log("Joined Room: " + roomNameInputField.text);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        Debug.LogError("Erorr: " + message);
        MenuManager.Instance.OpenMenu("Error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void LeaveMultiplayer()
    {
        //call timer
        mainCameraAnimation.SetBool("CameraUp", true);
        mainCameraAnimation.SetBool("CameraDown", false);
        startTimer3 = true;
        unloadingText.SetActive(true);
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        startTimer4 = true;
    }

    #region Update
    void Update()
    {
        #region Loading Wait Time

        if (startTimer)
        {
            timer += 1;
        }

        if (timer == 500)
        {  
            mainCameraAnimation.SetBool("CameraUp", false);
            mainCameraAnimation.SetBool("CameraDown", true);
        }

        if (timer == 505)
        {
            loadingText.SetActive(false);
            startTimer = false;
            timer = 0;
        }

        #endregion

        #region Unloading Wait Time

        if (startTimer3)
        {
            timer3 += 1;
        }

        if (timer3 == 500)
        {
            mainCameraAnimation.SetBool("CameraUp", false);
            mainCameraAnimation.SetBool("CameraDown", true);
        }

        if (timer3 == 505)
        {
            unloadingText.SetActive(false);
            startTimer3 = false;
            timer3 = 0;
        }

        #endregion

        #region Camera Down Wait Time

        if (startTimer2)
        {
            timer2 += 1;
        }

        if (timer2 == 615)
        {
            MenuManager.Instance.OpenMenu("Room Options");
            timer2 = 0;
            startTimer2 = false;
        }

        if (startTimer4)
        {
            timer4 += 1;
        }

        if (timer4 == 480)
        {
            menuController.BeginningScreen();
        }

        if (timer4 == 615)
        {
            timer4 = 0;
            startTimer4 = false;
        }

        #endregion
    }
    #endregion
}
