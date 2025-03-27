using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PhotonView))]
public class HPSystemRPCs : MonoBehaviourPunCallbacks
{
    private Action<float> _dagameRecievedAction = default;
    private Action<float> _dagameHealedAction = default;
    private Action  _restartHPAction = default;
    private HPSystem _hpSystem;
    private PhotonView _photonView;
    private NetworkCalls _networkCalls;


    public void Awake()
    {
        _networkCalls = new NetworkCalls(ENetworkType.PhotonNetwork);
        _hpSystem = GetComponent<HPSystem>();
        _photonView = GetComponent<PhotonView>();
        Assert.IsNotNull(_hpSystem, "HP system not detected on " + gameObject.name);
        Assert.IsNotNull(_photonView, "PhotonView not detected on " + gameObject.name);

    }

    public ETeams Team { get => _hpSystem.Team; }

    public override void OnEnable()
    {
        InitializeCalls();
    }

    public override void OnDisable()
    {
        RemoveCalls();
    }

    public void InitializeCalls()
    {
        if (_hpSystem == null)
        {
            throw new System.NullReferenceException("El objecto: "+ gameObject.name + " no tiene un hp system valido ");
        }

        _dagameHealedAction += _hpSystem.HealDamage;
        _dagameRecievedAction += _hpSystem.DealDamage;
        _restartHPAction += _hpSystem.RestartHP;
    }

    public void RemoveCalls()
    {
        if (_hpSystem == null)
        {
            throw new System.NullReferenceException("El objecto: " + gameObject.name + " no tiene un hp system valido ");
        }

        _dagameHealedAction -= _hpSystem.HealDamage;
        _dagameRecievedAction -= _hpSystem.DealDamage;
        _restartHPAction -= _hpSystem.RestartHP;
    }

    public void RecieveDamage(float damage)
    {
        if (!_networkCalls.IsMasterClient) { return; }
        _photonView.RPC("RecieveDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RecieveDamageRPC(float damage)
    {
        _dagameRecievedAction?.Invoke(damage);
    }

    public void HealDamage(float damage)
    {
        if (!_networkCalls.IsMasterClient) { return; }
        _photonView.RPC("HealDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    public void HealDamageRPC(float damage)
    {
        _dagameHealedAction?.Invoke(damage);
    }

    public void ResetHP()
    {
        if (!_networkCalls.IsMasterClient) { return; }
        _photonView.RPC("ResetHPRPC", RpcTarget.All);
    }

    [PunRPC]
    public void ResetHPRPC()
    {
        _restartHPAction?.Invoke();
    }

}
