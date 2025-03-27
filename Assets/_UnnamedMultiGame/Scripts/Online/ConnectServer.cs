using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectServer : MonoBehaviour
{
    [SerializeField]
    private ConnectionSignalSender _connectionSignalSender;
    void Start()
    {
        Screen.fullScreen = false;
        _connectionSignalSender.OnRequestConnection();
    }
}
