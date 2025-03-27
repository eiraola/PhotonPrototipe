using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "ConnectionSignalSender", menuName = "ScriptableObjects/ConnectionSignal", order = 1)]
public class ConnectionSignalSender : ScriptableObject
{
    private Action _onConnectionRequested;
    private Action _onJoinLobbyRequested;
    private Action<string, RoomOptions, Action, Action> _onCreateRoomRequested;
    private Action<string, Action, Action> _onJoinRoomRequested;
    private Action<List<RoomInfo>> _onRoomListUpdate;
    private List<RoomInfo> _currentRoomList;
    #region Connection
    public void OnConnectionAddListener(Action onCompleteAction = null)
    {
        _onConnectionRequested += onCompleteAction;
    }

    public void OnConnectionRemoveListener(Action onCompleteAction = null)
    {
        _onConnectionRequested -= onCompleteAction;
    }

    public void OnRequestConnection()
    {
        _onConnectionRequested?.Invoke();
    }
    
    #endregion

    #region Lobby
    public void OnJoinLobbyAddListener(Action onCompleteAction = null)
    {
        _onJoinLobbyRequested += onCompleteAction;
    }

    public void OnJoinLobbyRemoveListener(Action onCompleteAction = null)
    {
        _onJoinLobbyRequested -= onCompleteAction;
    }

    public void OnRequestJoinLobby()
    {
        _onJoinLobbyRequested?.Invoke();
    }

    #endregion

    #region Room
    #region CreateRoom
    public void CreateRoomRequest(string roomName, RoomOptions roomOptions, Action onSucess = null, Action onError = null)
    {
        _onCreateRoomRequested.Invoke(roomName, roomOptions, onSucess, onError);
    }

    public void OnCreateRoomAddListener(Action<string,RoomOptions, Action, Action> onRequestAction)
    {
        _onCreateRoomRequested += onRequestAction;
    }

    public void OnCreateRoomRemoveListener(Action<string, RoomOptions, Action, Action> onRequestAction)
    {
        _onCreateRoomRequested -= onRequestAction;
    }
    #endregion
    #region JoinRoom

    public void OnJoinRoomAddListener(Action<string, Action, Action> onCompleteAction)
    {
        _onJoinRoomRequested += onCompleteAction;
    }

    public void OnJoinRoomRemoveListener(Action<string, Action, Action> onCompleteAction)
    {
        _onJoinRoomRequested -= onCompleteAction;
    }

    public void JoinRoomRequest(string roomName, Action onSucess = null, Action onError = null)
    {
        _onJoinRoomRequested?.Invoke(roomName, onSucess, onError);
    }
    #endregion
    #endregion
    #region RoomList
    public void OnRoomListUpdate(List<RoomInfo> newRoomList)
    {
        _currentRoomList = newRoomList;
        _onRoomListUpdate?.Invoke(_currentRoomList);
    }

    public void OnRoomListUpdateAddListener(Action<List<RoomInfo>> onUpdate)
    {
        _onRoomListUpdate += onUpdate;
    }

    public void OnRoomListUpdateRemoveistener(Action<List<RoomInfo>> onUpdate)
    {
        _onRoomListUpdate -= onUpdate;
    }

    public List<RoomInfo> GetRoomList()
    {
        return _currentRoomList;
    }

    #endregion
}

