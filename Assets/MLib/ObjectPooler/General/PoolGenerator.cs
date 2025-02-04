
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public class PoolGenerator<T>: MonoBehaviour where T : Component 
    {
        [SerializeField] protected T prefab;
        [SerializeField] protected int initValue;

        private Transform holder;
        private Stack<T> available;


        public void Initialize(Transform holder)
        {
            this.holder = holder;
            Initialize();
        }

        public virtual void Initialize()
        {
            available = new Stack<T>();

            for(int i = 0; i<initValue; i++)
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
