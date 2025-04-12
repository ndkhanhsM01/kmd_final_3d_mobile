
using System.Collections;
using UnityEngine;

namespace Monster.Bomb
{
    public class TimerExplosion: MonoBehaviour
    {
        [SerializeField] private BombMonster machine;
        [SerializeField] private RangeFloat interval;
        [SerializeField] private float delayContinue;

        private WaitForSeconds delay;

        private void Awake()
        {
            delay = new WaitForSeconds(delayContinue);
        }

        private void OnEnable()
        {
            StartCoroutine(IE_Loop());
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator IE_Loop()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval.GetRandomValue());

                machine.SwitchToState<ExplosiveState>();

                yield return delay;
            }
        }
    }
}