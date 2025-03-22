
using MLib;

public class PoolCanonBullet : PoolGenerator<CanonBullet>
{
    private void Awake()
    {
        Initialize(transform);
    }
    public override CanonBullet GetItem()
    {
        var bullet = base.GetItem();
        bullet.SetOnExpodeEnd(() =>
        {
            ReturnPool(bullet);
        });
        return bullet;
    }
}