using UnityEngine;
using TMPro;
using Photon.Realtime;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _newRoomName;
    [SerializeField]
    private ConnectionSignalSender _connectionSignalSender;
    /// <summary>
    /// Creates a new room with the defined room name
    /// </summary>
    public void CreateNewRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        _connectionSignalSender.CreateRoomRequest(_newRoomName.text, roomOptions);
    }
}
