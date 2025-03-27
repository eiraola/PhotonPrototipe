using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ListGameRooms : MonoBehaviour
{
    [SerializeField]
    private List<MenuRoomElement> _menuRoomElementlist;
    [SerializeField]
    private Transform _menuElementsContainer;
    [SerializeField]
    private ConnectionSignalSender _connectionSignalSender;

    private void Start()
    {
        OnRoomListUpdate(_connectionSignalSender.GetRoomList());
        _connectionSignalSender.OnRoomListUpdateAddListener(OnRoomListUpdate);
    }
    public  void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (MenuRoomElement element in _menuRoomElementlist) 
        {
            element.gameObject.SetActive(false);
        }
        int index = 0;
        foreach (RoomInfo roomInfo in roomList)
        {
            _menuRoomElementlist[index].gameObject.SetActive(true);
            _menuRoomElementlist[index].InitiateElement(roomInfo.Name, roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers);
        }
    }
}
