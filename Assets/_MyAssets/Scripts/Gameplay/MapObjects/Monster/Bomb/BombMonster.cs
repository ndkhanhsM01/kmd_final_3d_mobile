using UnityEngine;

namespace Monster.Bomb
{
    [System.Serializable]
    public class ContextParam: MonsterParam
    {
        public RangeFloat idleDuration;
    }
    public class BombMonster: MonsterStateMachine<ContextParam>
    {

    }
}
