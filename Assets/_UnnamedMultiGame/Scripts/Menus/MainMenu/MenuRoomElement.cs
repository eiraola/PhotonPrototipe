using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuRoomElement : MonoBehaviour
{
    [SerializeField]
    private ConnectionSignalSender _connectionSignalSender;
    [SerializeField]
    private TMP_Text _roomName;
    [SerializeField]
    private TMP_Text _numPlayers;
    [SerializeField]
    private Button _onJoinButton;

    public void InitiateElement(string name, string numPlayers)
    {
        if (!_roomName)
        {
            return;
        }

        if (!_numPlayers)
        {
            return;
        }
        
        _roomName.text = name;
        _numPlayers.text = numPlayers;
        _onJoinButton.onClick.AddListener(JoinRoom);
    }

    public void JoinRoom()
    {
        _connectionSignalSender.JoinRoomRequest(_roomName.text);
    }

    public void OnDisable()
    {
        _onJoinButton?.onClick.RemoveListener(JoinRoom);
    }
}

