public class LightAttackBehaviour : EnemyBehaviourBase
{
    public override void Enable()
    {
        _entity.lightAttackAction?.Invoke();
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
