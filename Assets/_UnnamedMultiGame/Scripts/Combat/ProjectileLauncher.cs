using System;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectileGO;
    [SerializeField]
    private Transform _firepoint;
    [SerializeField]
    private ETeams _team = ETeams.Enemies;
    
    public void LaunchProjectile()
    {
        if (_projectileGO == null)
        {
            return;
        }

        DamageDealer projectile = Instantiate(_projectileGO, _firepoint.position, _firepoint.rotation).GetComponent<DamageDealer>();
        projectile.Initialize(_team);

    }
}
