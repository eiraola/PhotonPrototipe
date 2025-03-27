using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] 
    private float _spawnTime = 5.0f;
    [SerializeField]
    private GameObject _enemyToSpawn;
    [SerializeField]
    private Transform _spawnPosition;
    private CancellationTokenSource _cancelTokenSource;
    private void Start()
    {

    }

    private async UniTaskVoid SpawnEnemies()
    {
        if (_enemyToSpawn == null)
        {
            return;
        }
        while (!_cancelTokenSource.Token.IsCancellationRequested)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            PhotonNetwork.Instantiate(_enemyToSpawn.name, _spawnPosition.position, _spawnPosition.rotation);
        }
       
    }
    private void OnEnable()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.enabled = false;
            return;
        }
        if (_cancelTokenSource != null)
        {
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }
        _cancelTokenSource = new CancellationTokenSource();
        SpawnEnemies().Forget();
    }

    private void OnDisable()
    {
        if(_cancelTokenSource != null)
        {
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }
    }
}
