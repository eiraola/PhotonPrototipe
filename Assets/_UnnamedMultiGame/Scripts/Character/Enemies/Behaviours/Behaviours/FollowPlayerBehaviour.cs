using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBehaviour : EnemyBehaviourBase
{
    public override void Enable()
    {
        return;
    }

    public override void Initialize(EntityDataSchema entityData, BehaviourSwapDataSchema swapData)
    {
        _entity = entityData; 
        _swapData = swapData;
    }

    public override void Update(float deltaTime)
    {
        Vector3 currentDir  = (_entity.target.position - _entity.transform.position).normalized;
        _entity.transform.position += currentDir * Time.deltaTime * _entity.speed;
    }
}
