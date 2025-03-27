using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class EnemyBehaviourBase 
{
    protected EntityDataSchema _entity;
    protected BehaviourSwapDataSchema _swapData;
    protected Dictionary<EnemyBehaviourBase, BehaviourSwapperBase> _swapConditions;
    public abstract void Initialize(EntityDataSchema entityData, BehaviourSwapDataSchema swapData);
    public abstract void Enable();
    public abstract void Update(float deltaTime);

    public virtual void Disable()
    {
        RestartSwapConditions();
    }

    public void SetSwapConditions(Dictionary<EnemyBehaviourBase, BehaviourSwapperBase> swapConditions)
    {
        _swapConditions = swapConditions;
    }

    public void RestartSwapConditions()
    {
        foreach (KeyValuePair<EnemyBehaviourBase, BehaviourSwapperBase> swapCondition in _swapConditions)
        {
            swapCondition.Value.ResetBehaviourSwaper();
        }
    }

    public EnemyBehaviourBase CheckNewBehaviour()
    {
        foreach (KeyValuePair<EnemyBehaviourBase, BehaviourSwapperBase> swapCondition in _swapConditions)
        {
            if (swapCondition.Value.SwapBehaviour(_entity, _swapData))
            {
                return swapCondition.Key;
            }
        }

        return null;
    }
}
