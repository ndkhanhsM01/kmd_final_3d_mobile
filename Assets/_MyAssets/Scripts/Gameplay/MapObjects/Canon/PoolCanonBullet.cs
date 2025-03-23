
using MLib;
using UnityEngine;


[CreateAssetMenu(fileName = "PoolCanonBullet", menuName = "Pool/CanonBullet")]
public class PoolCanonBullet : PoolBase<CanonBullet>
{
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