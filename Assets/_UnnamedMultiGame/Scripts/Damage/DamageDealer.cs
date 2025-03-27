using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private ETeams _team;
    [SerializeField]
    private bool _initialized = false;

    public ETeams Team { get => _team; set => _team = value; }

    public void Initialize(ETeams newTeam)
    {
        _team = newTeam;
        _initialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_initialized) { return; }
        if (!other.TryGetComponent<HPSystemRPCs>(out HPSystemRPCs hpSystem)) { return; }
        if (hpSystem.Team == _team) { return; }

        hpSystem.RecieveDamage(_damage);
       // Destroy(gameObject);
    }
}
