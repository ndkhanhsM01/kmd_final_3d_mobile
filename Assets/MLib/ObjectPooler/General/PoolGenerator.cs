
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public abstract class PoolGenerator: ScriptableObject
    {
        protected Transform holder;
        public Transform Holder => holder;
        public abstract void Initialize(Transform holder);
        public abstract void Initialize();
    }
}
