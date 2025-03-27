using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
using Cinemachine;
[RequireComponent(typeof(PhotonView))]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private List<Transform> _spawnPoint = new List<Transform>();
    [SerializeField]
    private CinemachineFreeLook _cinemachineCamera;
    [SerializeField]
    private CharacterRegisterSO _characterRegisterSO;
    [SerializeField]
    private PhotonView _photonView;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        GameObject newPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, _spawnPoint[PhotonNetwork.PlayerList.Length-1].position, Quaternion.identity);
        if (newPlayer == null || _cinemachineCamera == null)
        {
            return;
        }
        _cinemachineCamera.Follow = newPlayer.transform;
        _cinemachineCamera.LookAt = newPlayer.transform;
        SimpleLocomotion sp = newPlayer.GetComponent<SimpleLocomotion>();
        sp.Camera = Camera.main;
        _photonView.RPC("OnCharacterCreatedRPC", RpcTarget.All);

    }

    [PunRPC]
    public void OnCharacterCreatedRPC()
    {
        UpdatePlayersList();
    }

    private void UpdatePlayersList()
    {
        _characterRegisterSO.ResetPlayers();
        GameObject[] _playerGOs = GameObject.FindGameObjectsWithTag(Constants.Tags.TAG_PLAYER);
        for (int i = 0; i < _playerGOs.Length; i++)
        {
            PlayerGameplayInfo player = new PlayerGameplayInfo();
            player.transform = _playerGOs[i].transform;
            _characterRegisterSO.AddPlayer(player);
        }
    }
}
