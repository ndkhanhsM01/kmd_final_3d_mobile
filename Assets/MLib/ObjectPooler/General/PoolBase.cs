
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public class PoolBase<T>: PoolGenerator where T : Component 
    {
        [SerializeField] protected T prefab;
        [SerializeField] protected int initValue;

        private Stack<T> available;


        public override void Initialize(Transform holder)
        {
            this.holder = holder;
            InitItems();
        }

        public override void Initialize()
        {
            holder = new GameObject(this.GetType().Name).transform;

            InitItems();
        }

        private void InitItems()
        {
            available = new Stack<T>();

            for (int i = 0; i < initValue; i++)
            {
                SpawnNewItem();
            }
        }

        public virtual T GetItem()
        {
            if (available.Count <= 0)
                SpawnNewItem();

            T item = available.Pop();

            return item;
        }

        public virtual void ReturnPool(T item)
        {
            item.transform.parent = holder;
            item.gameObject.SetActive(false);
            available.Push(item);
        }

        protected virtual T SpawnNewItem()
        {
            T itemClone = Instantiate(prefab, holder);
            ReturnPool(itemClone);

            return itemClone;
        }
    }
}
