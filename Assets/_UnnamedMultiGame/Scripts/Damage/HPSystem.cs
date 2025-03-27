using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HPSystemRPCs))]
public class HPSystem : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private ETeams _team = ETeams.TeamA;
    [SerializeField]
    private float _maxHP = 1.0f;

    [Header("Callbacks")]
    [SerializeField]
    public UnityEvent<float, float> _onDamageRecieved = new UnityEvent<float, float>();

    [SerializeField]
    public UnityEvent<float, float> _onDamageHealed = new UnityEvent<float, float>();

    [SerializeField]
    public UnityEvent _onHPDeployed = new UnityEvent();

    private float _currentHP = 1.0f;

    public ETeams Team { get => _team; set => _team = value; }
    public float CurrentHP { get => _currentHP;}

    private void Start()
    {
        RestartHP();
    }

    /// <summary>
    /// Restarts the hp to the maximum value;
    /// </summary>

    public void RestartHP()
    {
        _currentHP = _maxHP;
        Debug.LogError("The current health is: " + CurrentHP);
    }

    /// <summary>
    /// Takes damage from any source
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(float damage) {

        _currentHP -=damage; 
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            _onHPDeployed?.Invoke();
        }

        _onDamageRecieved?.Invoke(_currentHP, damage);
        Debug.LogError("The current health is: " + CurrentHP);
    }

    /// <summary>
    /// Heals damage from any source
    /// </summary>
    /// <param name="damage"></param>
    public void HealDamage(float damage)
    {
        if (_currentHP == _maxHP) {
            return;
        }
        _currentHP += damage;
        if (_currentHP < _maxHP)
        {
            _currentHP = _maxHP;
        }
        _onDamageHealed?.Invoke(_currentHP, damage);
        Debug.LogError("The current health is: " + CurrentHP);
    }

}
