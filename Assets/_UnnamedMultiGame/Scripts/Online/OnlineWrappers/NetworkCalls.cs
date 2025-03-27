using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;

public enum ENetworkType
{
    PhotonNetwork
}
public class NetworkCalls
{
    private NetworkWrapperBase _currentNetworkWrapper;
    public NetworkCalls(ENetworkType type) { 
        switch (type)
        {
            case ENetworkType.PhotonNetwork:
                _currentNetworkWrapper = new PhotonCallsWrapper();
                break;
        }
    }

    public bool IsMasterClient => _currentNetworkWrapper.IsMasterClient(); 

    public List<PlayerGameplayInfo> GetPlayers => _currentNetworkWrapper.GetCurrentPlayers();

    public void SetPlayerList(List<PlayerGameplayInfo> newPlayerList) => _currentNetworkWrapper.SetPlayerList(newPlayerList);
}
