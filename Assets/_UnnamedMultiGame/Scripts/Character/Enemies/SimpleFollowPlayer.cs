using UnityEngine;

public class SimpleFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private GameObject _nearestPlayer;
    private NetworkCalls _networkCalls;

    private void Start()
    {
        _networkCalls = new NetworkCalls(ENetworkType.PhotonNetwork);
    }

    private void Update()
    {
        if (!_networkCalls.IsMasterClient) { return; }
        _nearestPlayer = FindNearestPlayer();

        transform.position += CurrentDesiredDirection() * _speed * Time.deltaTime;
    }

    private GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) { 
            return null;
        }

        GameObject currentNearestPlayer = players[0];
        float nearestDistance = Vector3.Distance(transform.position, currentNearestPlayer.transform.position);
        float currentDistance = 0.0f;
        foreach (GameObject player in players)
        {
            currentDistance = Vector3.Distance(transform.position, player.transform.position);
            if (currentDistance < nearestDistance)
            {
                nearestDistance = currentDistance;
                currentNearestPlayer = player;
            }
        }

        return currentNearestPlayer;
    }

    private Vector3 CurrentDesiredDirection()
    {
        if (_nearestPlayer == null)
        {
            return Vector3.zero;
        }

        return (_nearestPlayer.transform.position - transform.position).normalized;

    }
}
