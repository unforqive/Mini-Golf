using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
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
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    public EnterName enterName;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        alreadyConnected = false;
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public void JoinMultiplayer()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();

        //call timer
        mainCameraAnimation.SetBool("CameraUp", true);
        mainCameraAnimation.SetBool("CameraDown", false);
        startTimer = true;
        loadingText.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        if (!alreadyConnected)
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        startTimer2 = true;

        Debug.Log("Joined Lobby");
        alreadyConnected = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000"); 
        Debug.Log(PhotonNetwork.NickName);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        Debug.Log("Creating Room: " + roomNameInputField.text);
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room Menu");
        Debug.Log("Joined Room: " + roomNameInputField.text);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
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
        Debug.Log("Leaving Room");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Room Settings");
        Debug.Log("Left Room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void LeaveMultiplayer()
    {
        //call timer
        mainCameraAnimation.SetBool("CameraUp", true);
        mainCameraAnimation.SetBool("CameraDown", false);
        startTimer3 = true;
        unloadingText.SetActive(true);

        PhotonNetwork.Disconnect();
        Debug.Log("Disconnecting");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        MenuManager.Instance.OpenMenu("");
        Debug.Log("Disconnected from PUN server, Reason: " + cause);
        alreadyConnected = false;
        startTimer4 = true;
    }

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
}
