
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public class TestPool: MonoBehaviour
    {
        [SerializeField] private float radiusRandom = 3f;
        [SerializeField] private PoolTransformEx poolTransform;

        private Queue<Transform> usingItem;
        private void Awake()
        {
            poolTransform.Initialize(transform);
            usingItem = new();
        }

        [MButton]
        private void SpawnRamdom()
        {
            Transform unit = poolTransform.GetItem();

            Vector3 newPosition = transform.position + Random.insideUnitSphere * radiusRandom;
            unit.position = newPosition;
            unit.SetActive(true);

            usingItem.Enqueue(unit);
        }

        [MButton]
        private void ReturnItem()
        {
            if(usingItem.Count <= 0)
            {
                Debug.LogWarning("Queue empty, so cannot return");
                return;
            }

            Transform item = usingItem.Dequeue();
            poolTransform.ReturnPool(item);
        }
    }
}