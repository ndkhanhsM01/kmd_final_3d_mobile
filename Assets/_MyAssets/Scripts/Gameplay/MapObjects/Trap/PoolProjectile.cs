

using MLib;

public class PoolHostileProjectile : PoolGenerator<HostileProjectile>
{
    public override HostileProjectile GetItem()
    {
        var projectile = base.GetItem();
        projectile.SetOnStop(() =>
        {
            ReturnPool(projectile);
        });
        return projectile;
    }
}