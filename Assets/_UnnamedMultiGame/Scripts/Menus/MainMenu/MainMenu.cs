using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject createMenu;
    [SerializeField]
    private GameObject joinMenu;

    void Start()
    {
        ActivateMainMenu();
    }

    private void DissableMenus()
    {
        mainMenu.SetActive(false);
        createMenu.SetActive(false);
        joinMenu.SetActive(false);
    }

    public void ActivateMainMenu()
    {
        DissableMenus();
        mainMenu.SetActive(true);
    }

    public void ActivateCreateMenu()
    {
        DissableMenus();
        createMenu.SetActive(true);
    }

    public void ActivateJoinMenu()
    {
        DissableMenus();
        joinMenu.SetActive(true);
    }

    public void ExitGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            return;
        }

        Application.Quit();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Application.Quit();
    }


}
