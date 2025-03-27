using Cysharp.Threading.Tasks;
using System;
using static Unity.VisualScripting.Member;
using System.Threading;
using System.Diagnostics;

public class AttackFinishedBehaviourSwapper : BehaviourSwapperBase
{
    public override void ResetBehaviourSwaper()
    {
       
    }

    public override bool SwapBehaviour(EntityDataSchema entityData, BehaviourSwapDataSchema behaviourData)
    {
        return !entityData.isAtacking; 
    }
}
