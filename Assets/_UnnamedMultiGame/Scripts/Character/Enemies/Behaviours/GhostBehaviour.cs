using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField]
    private CharacterRegisterSO _characterRegister;
    [SerializeField]
    private AnimationCalls _animationCalls;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _attackRange = 1.0f;
    [SerializeField]
    private float _timeBetweenAttacks = 5.0f;
    //Data
    private BehaviourSwapDataSchema _behaviourSwapDataSchema = new BehaviourSwapDataSchema();
    private EntityDataSchema _behaviourDataSchema = new EntityDataSchema();
    //Behaviours
    private EnemyBehaviourBase _currentBehaviour = null;
    private FollowPlayerBehaviour _followPlayerBehaviour = new FollowPlayerBehaviour();
    private WaitTimeBehaviour _waitTimeBehaviour = new WaitTimeBehaviour();
    private LightAttackBehaviour _attackBehaviour = new LightAttackBehaviour();
    //BehaviourSwappers
    private PositionReachedBehaviourSwapper _positionReachedBehaviourSwapper = new PositionReachedBehaviourSwapper();
    private WaitTimeBehaviourSwapper _waitTimeBehaviourSwapper = new WaitTimeBehaviourSwapper();
    private AttackFinishedBehaviourSwapper _attackFinishedBehaviourSwapper = new AttackFinishedBehaviourSwapper();
    private PositionOutOfReachBehaviourSwapper _targetOutOfReachBehaviourSwapper = new PositionOutOfReachBehaviourSwapper();
    //Behaviour Swap HashTables
    private Dictionary<EnemyBehaviourBase, BehaviourSwapperBase> _lightAttackBehaviourSwappers = new Dictionary<EnemyBehaviourBase, BehaviourSwapperBase>();
    private Dictionary<EnemyBehaviourBase, BehaviourSwapperBase> _followPlayerSwappers = new Dictionary<EnemyBehaviourBase, BehaviourSwapperBase>();
    private Dictionary<EnemyBehaviourBase, BehaviourSwapperBase> _waitTimeSwappers = new Dictionary<EnemyBehaviourBase, BehaviourSwapperBase>();
    private NetworkCalls _networkCalls = new NetworkCalls(ENetworkType.PhotonNetwork);
    void Start()
    {
        _behaviourDataSchema.transform = transform;
        _behaviourDataSchema.speed = _speed;
        _behaviourDataSchema.lightAttackAction += LaunchAttack;
        _behaviourSwapDataSchema.distance = _attackRange;
        _behaviourSwapDataSchema.time = _timeBetweenAttacks;
        //initialize behaviours
        _followPlayerBehaviour.Initialize(_behaviourDataSchema, _behaviourSwapDataSchema);
        _waitTimeBehaviour.Initialize(_behaviourDataSchema, _behaviourSwapDataSchema);
        _attackBehaviour.Initialize(_behaviourDataSchema, _behaviourSwapDataSchema);
        //Set behaviourSwappers
        _lightAttackBehaviourSwappers.Add(_waitTimeBehaviour, _attackFinishedBehaviourSwapper);
        _attackBehaviour.SetSwapConditions(_lightAttackBehaviourSwappers);

        _followPlayerSwappers.Add(_waitTimeBehaviour, _positionReachedBehaviourSwapper);
        _followPlayerBehaviour.SetSwapConditions(_followPlayerSwappers);

        _waitTimeSwappers.Add(_followPlayerBehaviour, _targetOutOfReachBehaviourSwapper);
        _waitTimeSwappers.Add(_attackBehaviour, _waitTimeBehaviourSwapper);
        _waitTimeBehaviour.SetSwapConditions(_waitTimeSwappers);
        //SetInitialSwapper
        SetCurrentBehaviour(_followPlayerBehaviour);
    }

    private void OnDestroy()
    {
        _behaviourDataSchema.lightAttackAction -= LaunchAttack;
    }

    void Update()
    {
        if(!_networkCalls.IsMasterClient){
            this.enabled = false;
            return;
        }

        SetClosestPlayer();
        _currentBehaviour.Update(Time.deltaTime);
        CheckBehaviourSwap();
    }

    private void CheckBehaviourSwap()
    {
        EnemyBehaviourBase newbehaviour = _currentBehaviour.CheckNewBehaviour();
        if (newbehaviour == null)
        {
            return;
        }
        SetCurrentBehaviour(newbehaviour);
    }

    private void SetCurrentBehaviour(EnemyBehaviourBase newBehaviour)
    {
        if (_currentBehaviour != null)
        {
            _currentBehaviour.Disable();
        }
        _currentBehaviour = newBehaviour;
        _currentBehaviour.Enable();
        
    }

    private void SetClosestPlayer()
    {
        if (_characterRegister.Players.Count < 1)
        {
            _behaviourDataSchema.target = transform;
            return;
        }

        Transform target = _characterRegister.Players[0].transform;
        float closestDistance = Vector3.Distance(transform.position, target.position);

        foreach (PlayerGameplayInfo player in _characterRegister.Players)
        {
            float currentDistance = Vector3.Distance(transform.position, player.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                target = player.transform;
            }
        }

        _behaviourDataSchema.target = target.transform;
    }

    public void LaunchAttack()
    {
        _animationCalls.DoAttackAnim();
        _behaviourDataSchema.isAtacking = true;
    }

    public void EndAttack()
    {
        _behaviourDataSchema.isAtacking = false;
    }
}
