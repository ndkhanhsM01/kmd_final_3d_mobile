
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
        bullet.SetOnReturnPool(() =>
        {
            ReturnPool(bullet);
        });
        return bullet;
    }
}