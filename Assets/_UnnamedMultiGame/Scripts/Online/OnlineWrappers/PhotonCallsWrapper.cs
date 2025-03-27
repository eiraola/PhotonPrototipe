using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCallsWrapper : NetworkWrapperBase
{
    public override List<PlayerGameplayInfo> GetCurrentPlayers()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsMasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public override void SetPlayerList(List<PlayerGameplayInfo> newPlayerList)
    {
        throw new System.NotImplementedException();
    }
}
