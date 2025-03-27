using Cysharp.Threading.Tasks;
using System;
using static Unity.VisualScripting.Member;
using System.Threading;
using System.Diagnostics;

public class WaitTimeBehaviourSwapper : BehaviourSwapperBase
{
    bool timePassed = false;
    bool started = false;
    float timeRemaining = 0;
    CancellationTokenSource _source;
    public override bool SwapBehaviour(EntityDataSchema entityData, BehaviourSwapDataSchema behaviourData)
    {
        if (!started)
        {

            timeRemaining = behaviourData.time;
            started = true;
            _source = new CancellationTokenSource();
            CountTime().Forget();
        }

        return timePassed;
    }

    public async UniTask CountTime()
    {
        while (timeRemaining > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1), ignoreTimeScale: false, cancellationToken: _source.Token);
            timeRemaining -= 0.1f;
        }
        timePassed = true;
    }

    public override void ResetBehaviourSwaper()
    {
        timePassed = false;
        started = false;
        timeRemaining = 0;
        if (_source != null)
        {
            _source.Cancel();
            _source.Dispose();
        }
    }
}
