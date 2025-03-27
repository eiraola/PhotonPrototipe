using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class WaitTimeBehaviour : EnemyBehaviourBase
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
        return;
    }
}
