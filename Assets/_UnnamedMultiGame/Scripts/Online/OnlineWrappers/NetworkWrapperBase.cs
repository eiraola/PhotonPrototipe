using System.Collections.Generic;

public abstract class NetworkWrapperBase 
{
    private static List<PlayerGameplayInfo> _newPlayerList = new List<PlayerGameplayInfo>();
    public abstract bool IsMasterClient();
    public abstract List<PlayerGameplayInfo> GetCurrentPlayers();
    public abstract void SetPlayerList(List<PlayerGameplayInfo> newPlayerList);
}
