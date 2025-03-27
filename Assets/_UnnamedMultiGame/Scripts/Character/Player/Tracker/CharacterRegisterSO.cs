using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterRegister", menuName = "ScriptableObjects/CharacterRegister", order = 1)]
public class CharacterRegisterSO : ScriptableObject
{
    private List<PlayerGameplayInfo> _players = new List<PlayerGameplayInfo>();

    public List<PlayerGameplayInfo> Players { get => _players;}

    public void ResetPlayers()
    {
        _players = new List<PlayerGameplayInfo>();
    }

    public void AddPlayer(PlayerGameplayInfo _player)
    {
        if (_players.Contains(_player))
        {
            return;
        }

        _players.Add(_player);
    }

    public void RemovePlayer(PlayerGameplayInfo _player)
    {
        if (!_players.Contains(_player))
        {
            return;
        }

        _players.Remove(_player);
    }
}
