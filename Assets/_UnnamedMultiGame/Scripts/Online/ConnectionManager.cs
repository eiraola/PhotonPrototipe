using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private static int numInstances = 0;

    [SerializeField] private ConnectionSignalSender _coneccionSignalSender;
    [SerializeField] private CharacterRegisterSO _characterRegisterSO;

    //CreateRoomCallbacks
    private Action _onCreateRoomSuccess = null;
    private Action _onCreateRoomFailure = null;
    //JoinRoomCallbacks
    private Action _onJoinRoomSuccess = null;
    private Action _onJoinRoomFailure = null;

    #region UnityCallbacks

    private void Start()
    {
        numInstances++;

        if (numInstances > 1)
        {
            numInstances--;
            Destroy(this.gameObject);
            return;
        }
        _coneccionSignalSender.OnRoomListUpdate(new List<RoomInfo>());
        DontDestroyOnLoad(this.gameObject);
    }

    public void Awake()
    {
        AddSignalListeners();
    }

    public void OnDestroy()
    {
        RemoveSignalListeners();
    }

    #endregion
    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateNewRoom(string roomName, RoomOptions roomOptions, Action onSucess = null, Action onError = null)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        _onCreateRoomSuccess += onSucess;
        _onCreateRoomFailure += onError;
    }

    public void JoinRoom(string roomName, Action onSucess = null, Action onError = null)
    {
        PhotonNetwork.JoinRoom(roomName);
        _onJoinRoomSuccess += onSucess;
        _onJoinRoomFailure += onError;
    }


    #region Callbacks

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGameRoom");
        _onJoinRoomSuccess?.Invoke();
        RemoveJoinRoomCallbacks();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _onCreateRoomFailure?.Invoke();
        RemoveCreateRoomCallbacks();
    }

    public override void OnCreatedRoom()
    {
        _onCreateRoomSuccess?.Invoke();
        RemoveCreateRoomCallbacks();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _onJoinRoomFailure?.Invoke();
        RemoveJoinRoomCallbacks();
    }

    private void RemoveJoinRoomCallbacks() {
        _onJoinRoomSuccess = null;
        _onJoinRoomFailure = null;
    }

    private void RemoveCreateRoomCallbacks()
    {
        _onCreateRoomSuccess = null;
        _onCreateRoomFailure = null;
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _coneccionSignalSender.OnRoomListUpdate(roomList);
    }


    private void AddSignalListeners()
    {
        _coneccionSignalSender.OnConnectionAddListener(ConnectToPhoton);
        _coneccionSignalSender.OnCreateRoomAddListener(CreateNewRoom);
        _coneccionSignalSender.OnJoinLobbyAddListener(JoinLobby);
        _coneccionSignalSender.OnJoinRoomAddListener(JoinRoom);
    }

    private void RemoveSignalListeners()
    {
        _coneccionSignalSender.OnConnectionRemoveListener(ConnectToPhoton);
        _coneccionSignalSender.OnCreateRoomRemoveListener(CreateNewRoom);
        _coneccionSignalSender.OnJoinLobbyRemoveListener(JoinLobby);
        _coneccionSignalSender.OnJoinRoomRemoveListener(JoinRoom);
    }
    #endregion
}
