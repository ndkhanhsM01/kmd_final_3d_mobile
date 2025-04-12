
namespace Monster.Bomb
{
    public class MoveState : BaseState<ContextParam>
    {
        public MoveState(MonsterStateMachine<ContextParam> context, ContextParam contextParam) 
            : base(context, contextParam)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
        }
    }
}