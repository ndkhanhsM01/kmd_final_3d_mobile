using UnityEngine;
using DG.Tweening;
using MLib;

[CreateAssetMenu(fileName = "PoolHostileProjectile", menuName = "Pool/HostileProjectile")]
public class PoolHostileProjectile : PoolBase<HostileProjectile>
{
    [SerializeField] private float maxLifeTimeOnStop = 1f;
    public override HostileProjectile GetItem()
    {
        var projectile = base.GetItem();
        projectile.SetOnStop(() =>
        {
            DelayReturn(projectile);
        });
        return projectile;
    }

    private void DelayReturn(HostileProjectile projectile)
    {
        if (projectile == null) return;

        DOVirtual.DelayedCall(maxLifeTimeOnStop, () =>
        {
            ReturnPool(projectile);
        });
    }
}