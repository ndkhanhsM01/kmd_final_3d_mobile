
using UnityEngine;

namespace MLib
{
    public class PoolManager: MSingleton<PoolManager>
    {
        [SerializeField] private PoolGenerator[] pools;

        protected override void Awake()
        {
            base.Awake();
            Transform parentAll = transform;
            foreach (var pool in pools)
            {
                pool.Initialize();
                pool.Holder.parent = parentAll;
            }
        }
    }
}